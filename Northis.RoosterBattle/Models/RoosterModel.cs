using System;
using System.Collections.Generic;
using System.Linq;
using Catel.Data;
using Northis.RoosterBattle.GameServer;

namespace Northis.RoosterBattle.Models
{
	/// <summary>
	/// Предоставляет модель петуха.
	/// </summary>
	/// <seealso cref="ValidatableModelBase" />
	internal class RoosterModel : ValidatableModelBase, ICloneable
	{
		#region Fields
		#region Limiters
		private readonly int _minWeight = 2;
		private int _maxWeight = 8;
		private readonly int _maxHeight = 50;
		private readonly int _minHeight = 20;
		private int _maxHealth = 100;
		private readonly int _maxStamina = 100;
		private int _maxThickness = 30;
		private int _maxLuck = 30;
		private int _maxBrickness = 30;
		#endregion

		#region Consts
		private const int DefaultMaxHealth = 100;
		private const int DefaultMaxWeight = 8;
		private const int DefaultMaxThickness = 30;
		private const int DefaultMaxBrickness = 30;
		private const int DefaultMaxLuck = 30;
		#endregion

		private readonly Random _luckMeter = new Random();
		private readonly Random _bricknessMeter = new Random();
		/// <summary>
		/// Словарь с делегатами цветовых модификаций петухов.
		/// </summary>
		private readonly Dictionary<RoosterColor, Action> ColorModifications;
		/// <summary>
		/// Словарь с делегатами очистки цветовых модификаций петухов.
		/// </summary>
		private readonly Dictionary<RoosterColor, Action> ClearModifications;

		#region Static		
		/// <summary>
		/// Зарегистрированное свойство "Вес" петуха.
		/// </summary>
		public static readonly PropertyData WeightProperty = RegisterProperty(nameof(Weight), typeof(double));
		/// <summary>
		/// Зарегистрированное свойство "Высота" петуха.
		/// </summary>
		public static readonly PropertyData HeightProperty = RegisterProperty(nameof(Height), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Здоровье" петуха.
		/// </summary>
		public static readonly PropertyData HealthProperty = RegisterProperty(nameof(Health), typeof(double));
		/// <summary>
		/// Зарегистрированное свойство "Выносливость" петуха.
		/// </summary>
		public static readonly PropertyData StaminaProperty = RegisterProperty(nameof(Stamina), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Окрас" петуха.
		/// </summary>
		public static readonly PropertyData ColorProperty = RegisterProperty(nameof(Color), typeof(RoosterColor));
		/// <summary>
		/// Зарегистрированное свойство "Юркость" петуха.
		/// </summary>
		public static readonly PropertyData BricknessProperty = RegisterProperty(nameof(Brickness), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Броня" петуха.
		/// </summary>
		public static readonly PropertyData CrestProperty = RegisterProperty(nameof(Crest), typeof(CrestSize));
		/// <summary>
		/// Зарегистрированное свойство "Плотность" петуха.
		/// </summary>
		public static readonly PropertyData ThicknessProperty = RegisterProperty(nameof(Thickness), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Везение" петуха.
		/// </summary>
		public static readonly PropertyData LuckProperty = RegisterProperty(nameof(Luck), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Имя" петуха.
		/// </summary>
		public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string));
		/// <summary>
		/// Зарегистрированное свойство "Количество побед" петуха.
		/// </summary>
		public static readonly PropertyData WinStreakProperty = RegisterProperty(nameof(WinStreak), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Сила удара" петуха.
		/// </summary>
		public static readonly PropertyData HitProperty = RegisterProperty(nameof(Hit), typeof(double));
		/// <summary>
		/// Зарегистрированное свойство "Урон" петуха.
		/// </summary>
		public static readonly PropertyData DamageProperty = RegisterProperty(nameof(Damage), typeof(double));
		/// <summary>
		/// Зарегистрированное свойство "Максимальное здоровье" петуха.
		/// </summary>
		public static readonly PropertyData MaxHealthProperty = RegisterProperty(nameof(MaxHealth), typeof(int));
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализует новый объект класса <see cref="RoosterModel" />.
		/// </summary>
		public RoosterModel()
		{
			Health = 100;
			MaxHealth = 100;
			Stamina = 100;

			ColorModifications = new Dictionary<RoosterColor, Action>
			{
				{
					RoosterColor.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, 120);
						MaxHealth = _maxHealth;
						Health = _maxHealth;
					}
				},
				{
					RoosterColor.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, 50)
				},
				{
					RoosterColor.White, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, 10)
				},
				{
					RoosterColor.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, 50)
				},
				{
					RoosterColor.Black, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, 50)
				}
			};

			ClearModifications = new Dictionary<RoosterColor, Action>
			{
				{
					RoosterColor.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, DefaultMaxHealth);
						MaxHealth = DefaultMaxHealth;
					}
				},
				{
					RoosterColor.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, DefaultMaxBrickness)
				},
				{
					RoosterColor.Black, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, DefaultMaxLuck)
				},
				{
					RoosterColor.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, DefaultMaxThickness)
				},
				{
					RoosterColor.White, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, DefaultMaxWeight)
				}
			};
		}

		public RoosterModel(RoosterDto rooster) : this()
		{
			if (rooster != null)
			{
				Health = rooster.Health;
				MaxHealth = rooster.MaxHealth;
				Stamina = rooster.Stamina;
				Brickness = rooster.Brickness;
				Luck = rooster.Luck;
				Thickness = rooster.Thickness;
				Color = ColorParse(rooster.ColorDto);
				Crest = SizeParse(rooster.Crest);
				Height = rooster.Height;
				Weight = rooster.Weight;
				Name = rooster.Name;
				WinStreak = rooster.WinStreak;
			}
		}

		#endregion

		#region Properties		
		/// <summary>
		/// Возвращает или устанавливает имя петуха.
		/// </summary>
		/// <value>
		/// Имя петуха.
		/// </value>
		public string Name
		{
			get => GetValue<string>(NameProperty);
			set => SetValue(NameProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает количество побед петуха.
		/// </summary>
		/// <value>
		/// Количество побед.
		/// </value>
		public int WinStreak
		{
			get => GetValue<int>(WinStreakProperty);
			set => SetValue(WinStreakProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает вес петуха.
		/// </summary>
		/// <value>
		/// Вес петуха.
		/// </value>

		public double Weight
		{
			get => GetValue<double>(WeightProperty);
			set
			{
				SetValue(WeightProperty, Clamp(value, _minWeight, _maxWeight));
				UpdateDamage();
			}
		}

		/// <summary>
		/// Возвращает или устанавливает высоту петуха.
		/// </summary>
		/// <value>
		/// Высота петуха.
		/// </value>
		public int Height
		{
			get => GetValue<int>(HeightProperty);
			set
			{
				SetValue(HeightProperty, Clamp(value, _minHeight, _maxHeight));
				UpdateDamage();
			}
		}

		/// <summary>
		/// Возвращает или устанавливает здоровье петуха.
		/// </summary>
		/// <value>
		/// Здоровье петуха.
		/// </value>
		public double Health
		{
			get => GetValue<double>(HealthProperty);
			set => SetValue(HealthProperty, Clamp(value, 0, _maxHealth));
		}

		/// <summary>
		/// Возвращает или устанавливает максимальное здоровье петуха.
		/// </summary>
		/// <value>
		/// The maximum health.
		/// </value>
		public int MaxHealth
		{
			get => GetValue<int>(MaxHealthProperty);
			set => SetValue(MaxHealthProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает выносливость петуха.
		/// </summary>
		/// <value>
		/// Выносливость петуха.
		/// </value>
		public int Stamina
		{
			get => GetValue<int>(StaminaProperty);
			set => SetValue(StaminaProperty, Clamp(value, 0, _maxStamina));
		}

		/// <summary>
		/// Возвращает или устанавливает окрас петуха.
		/// </summary>
		/// <value>
		/// Окрас петуха.
		/// </value>
		public RoosterColor Color
		{
			get => GetValue<RoosterColor>(ColorProperty);
			set
			{
				SetValue(ColorProperty, value);
				OnColorChange();
				UpdateDamage();
			}
		}

		/// <summary>
		/// Возвращает или устанавливает юркость петуха.
		/// </summary>
		/// <value>
		/// Юркость петуха.
		/// </value>
		public int Brickness
		{
			get => GetValue<int>(BricknessProperty);

			set => SetValue(BricknessProperty, Clamp(value, 0, _maxBrickness));
		}

		/// <summary>
		/// Возвращает или устанавливает броню петуха.
		/// </summary>
		/// <value>
		/// Броня петуха.
		/// </value>
		public CrestSize Crest
		{
			get => GetValue<CrestSize>(CrestProperty);
			set
			{
				SetValue(CrestProperty, value);
				UpdateDamage();
			}
		}

		/// <summary>
		/// Возвращает или устанавливает плотность петуха.
		/// </summary>
		/// <value>
		/// Плотность петуха.
		/// </value>
		public int Thickness
		{
			get => GetValue<int>(ThicknessProperty);
			set => SetValue(ThicknessProperty, Clamp(value, 0, _maxThickness));
		}

		/// <summary>
		/// Возвращает или устанавливает удачу петуха.
		/// </summary>
		/// <value>
		/// Удачливость петуха.
		/// </value>
		public int Luck
		{
			get => GetValue<int>(LuckProperty);

			set => SetValue(LuckProperty, Clamp(value, 0, _maxLuck));
		}

		/// <summary>
		/// Возвращает или устанавливает урон петуха.
		/// </summary>
		public double Damage
		{
			get
			{
				//Базовое значение урона от 1 до 4 (до 5 для Черного тяжеловеса)  
				var dmg = Weight / _minWeight;
				//Усиление от 0 до 25%
				dmg *= (double) Height / _minHeight / 10 + 1;
				//Усиление от 0 до 50%
				dmg *= (double) CalcEnumIndex(Crest) / 4 + 1;

				return Math.Round(dmg, 2) + WinStreak;
			}
			set => SetValue(DamageProperty, value);
		}

		/// <summary>
		/// Возвращает вычисленную силу удара петуха.
		/// </summary>
		public double Hit
		{
			get
			{
				var totalDamage = 0.0;
				var luck = _luckMeter.Next(0, 100);
				if (luck <= Luck)
				{
					totalDamage += 2 * Damage;
				}
				else
				{
					totalDamage = Damage;
				}

				if (Stamina == 0)
				{
					totalDamage /= 2;
				}

				return totalDamage;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Принимает удар от другого петуха.
		/// </summary>
		/// <param name="sender">Ударивший петух.</param>
		public void TakeHit(RoosterModel sender)
		{
			if (_bricknessMeter.Next(0, 100) >= Brickness || Stamina == 0)
			{
				Health -= sender.Hit * (1 - (double) Thickness / 100);
				Health = Math.Round(Health, 2);
			}

			Stamina -= 5;
		}

		/// <summary>
		/// Создает новый объект, являющийся копией текущего экземпляра.
		/// </summary>
		/// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
		public object Clone() =>
			new RoosterModel
			{
				Health = Health,
				Stamina = Stamina,
				Color = Color,
				Crest = Crest,
				Brickness = Brickness,
				Weight = Weight,
				Height = Height,
				Luck = Luck,
				Thickness = Thickness,
				Name = Name,
				WinStreak = WinStreak,
				Damage = Damage
			};
		#endregion

		#region Private Methods
		/// <summary>
		/// Выполняет повторное вычисление урона.
		/// </summary>
		private void UpdateDamage() => SetValue(DamageProperty, Damage);

		/// <summary>
		/// Выполняет ограничение значения минимальным и максимальным уровнем.
		/// </summary>
		/// <param name="value">Само значение.</param>
		/// <param name="min">Возможный минимум.</param>
		/// <param name="max">Возможный максимум.</param>
		/// <returns></returns>
		private T Clamp<T>(T value, T min, T max) where T : IComparable => value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;

		/// <summary>
		/// Вычисляет порядковый индекс значения перечисления.
		/// </summary>
		/// <param name="first">The first.</param>
		/// <returns></returns>
		private int CalcEnumIndex(Enum first)
		{
			var names = Enum.GetNames(first.GetType())
							.ToList();
			return names.IndexOf(first.ToString());
		}

		/// <summary>
		/// Выполняет смену модификаций при изменении цвета.
		/// </summary>
		private void OnColorChange()
		{
			ClearColorModifications();
			ColorModifications[Color]
				.Invoke();
		}

		/// <summary>
		/// Выполняет очистку всех модификаций.
		/// </summary>
		private void ClearColorModifications()
		{
			foreach (var clearModification in ClearModifications)
			{
				if (clearModification.Key != Color)
				{
					clearModification.Value();
				}
			}
		}

		/// <summary>
		/// Выполняет смену максимального порога значения.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <param name="newValue">The new value.</param>
		private void ChangeMaxLimit(string propertyName, int minValue, ref int maxValue, int newValue)
		{
			var property = GetType()
						   .GetProperties()
						   .First(x => x.Name == propertyName);
			maxValue = newValue;
			if (property.PropertyType == typeof(double))
			{
				property.SetValue(this, Clamp((double) property.GetValue(this), minValue, maxValue));
			}
		}

		public RoosterDto ToRoosterDto() =>
			new RoosterDto
			{
				Height = Height,
				ColorDto = ColorDtoParse(Color),
				Health = Health,
				Stamina = Stamina,
				Brickness = Brickness,
				Crest = SizeDtoParse(Crest),
				Weight = Weight,
				WinStreak = WinStreak,
				Luck = Luck,
				Name = Name,
				Thickness = Thickness,
				MaxHealth = MaxHealth,
				Damage = Damage,
				Hit = Hit
			};

		private RoosterColor ColorParse(RoosterColorDto color)
		{
			if (RoosterColor.TryParse(color.ToString(), out RoosterColor outColor))
				return outColor;
			throw new ArgumentException();
		}

		private RoosterColorDto ColorDtoParse(RoosterColor color)
		{
			if (RoosterColorDto.TryParse(color.ToString(), out RoosterColorDto outColor))
				return outColor;
			throw new ArgumentException();
		}

		private CrestSize SizeParse(CrestSizeDto size)
		{
			if (CrestSize.TryParse(size.ToString(), out CrestSize outSize))
				return outSize;
			throw new ArgumentException();
		}
		private CrestSizeDto SizeDtoParse(CrestSize size)
		{
			if (CrestSizeDto.TryParse(size.ToString(), out CrestSizeDto outSize))
				return outSize;
			throw new ArgumentException();
		}
		#endregion
	}
}
