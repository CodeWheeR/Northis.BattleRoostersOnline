using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferObjects;

namespace Northis.BattleRoostersOnline.Models
{
	/// <summary>
	/// Предоставляет модель петуха.
	/// </summary>
	/// <seealso cref="ValidatableModelBase" />
	internal class RoosterModel : ICloneable
	{
		#region Fields
		private double _weight;
		private int _height;
		private double _health;
		private int _stamina;
		private RoosterColorDto _color;
		private int _brickness;
		private int _luck;
		private int _thickness;

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
		private readonly Dictionary<RoosterColorDto, Action> ColorModifications;
		/// <summary>
		/// Словарь с делегатами очистки цветовых модификаций петухов.
		/// </summary>
		private readonly Dictionary<RoosterColorDto, Action> ClearModifications;
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

			ColorModifications = new Dictionary<RoosterColorDto, Action>
			{
				{
					RoosterColorDto.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, 120);
						MaxHealth = _maxHealth;
						Health = _maxHealth;
					}
				},
				{
					RoosterColorDto.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, 50)
				},
				{
					RoosterColorDto.White, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, 10)
				},
				{
					RoosterColorDto.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, 50)
				},
				{
					RoosterColorDto.Black, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, 50)
				}
			};

			ClearModifications = new Dictionary<RoosterColorDto, Action>
			{
				{
					RoosterColorDto.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, DefaultMaxHealth);
						MaxHealth = DefaultMaxHealth;
					}
				},
				{
					RoosterColorDto.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, DefaultMaxBrickness)
				},
				{
					RoosterColorDto.Black, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, DefaultMaxLuck)
				},
				{
					RoosterColorDto.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, DefaultMaxThickness)
				},
				{
					RoosterColorDto.White, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, DefaultMaxWeight)
				}
			};
		}

		public RoosterModel(RoosterDto rooster) : this()
		{
			Health = rooster.Health;
			MaxHealth = rooster.MaxHealth;
			Stamina = rooster.Stamina;
			Brickness = rooster.Brickness;
			Luck = rooster.Luck;
			Thickness = rooster.Thickness;
			Color = rooster.ColorDto;
			Crest = rooster.Crest;
			Height = rooster.Height;
			Weight = rooster.Weight;
			Name = rooster.Name;
			WinStreak = rooster.WinStreak;
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
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает количество побед петуха.
		/// </summary>
		/// <value>
		/// Количество побед.
		/// </value>
		public int WinStreak
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает вес петуха.
		/// </summary>
		/// <value>
		/// Вес петуха.
		/// </value>
		public double Weight
		{
			get => _weight;
			set => _weight = Clamp(value, _minWeight, _maxWeight);
		}

		/// <summary>
		/// Возвращает или устанавливает высоту петуха.
		/// </summary>
		/// <value>
		/// Высота петуха.
		/// </value>
		public int Height
		{
			get => _height;
			set => _height = Clamp(value, _minHeight, _maxHeight);
		}

		/// <summary>
		/// Возвращает или устанавливает здоровье петуха.
		/// </summary>
		/// <value>
		/// Здоровье петуха.
		/// </value>
		public double Health
		{
			get => _health;
			set => _health = Clamp(value, 0, _maxHealth);
		}

		/// <summary>
		/// Возвращает или устанавливает максимальное здоровье петуха.
		/// </summary>
		/// <value>
		/// The maximum health.
		/// </value>
		public int MaxHealth
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает выносливость петуха.
		/// </summary>
		/// <value>
		/// Выносливость петуха.
		/// </value>
		public int Stamina
		{
			get => _stamina;
			set => _stamina = Clamp(value, 0, _maxStamina);
		}

		/// <summary>
		/// Возвращает или устанавливает окрас петуха.
		/// </summary>
		/// <value>
		/// Окрас петуха.
		/// </value>
		public RoosterColorDto Color
		{
			get => _color;
			set
			{
				_color = value;
				OnColorChange();
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
			get => _brickness;
			set => _brickness = Clamp(value, 0, _maxBrickness);
		}

		/// <summary>
		/// Возвращает или устанавливает броню петуха.
		/// </summary>
		/// <value>
		/// Броня петуха.
		/// </value>
		public CrestSizeDto Crest
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает плотность петуха.
		/// </summary>
		/// <value>
		/// Плотность петуха.
		/// </value>
		public int Thickness
		{
			get => _thickness;
			set => _thickness = Clamp(value, 0, _maxThickness);
		}

		/// <summary>
		/// Возвращает или устанавливает удачу петуха.
		/// </summary>
		/// <value>
		/// Удачливость петуха.
		/// </value>
		public int Luck
		{
			get => _luck;

			set => _luck = Clamp(value, 0, _maxLuck);
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
		public RoosterDto ToRoosterDto() =>
			new RoosterDto
			{
				Height = Height,
				ColorDto = Color,
				Health = Health,
				Stamina = Stamina,
				Brickness = Brickness,
				Crest = Crest,
				Weight = Weight,
				WinStreak = WinStreak,
				Luck = Luck,
				Name = Name,
				Thickness = Thickness,
				MaxHealth = MaxHealth,
				Damage = Damage,
				Hit = Hit
			};

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
				WinStreak = WinStreak
			};
		#endregion

		#region Private Methods
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
		#endregion
	}
}
