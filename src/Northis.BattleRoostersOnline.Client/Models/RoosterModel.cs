using System;
using System.Collections.Generic;
using System.Linq;
using Catel.Data;
using Northis.BattleRoostersOnline.Client.GameServer;

namespace Northis.BattleRoostersOnline.Client.Models
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
		/// Зарегистрированное свойство "Максимальное здоровье" петуха.
		/// </summary>
		public static readonly PropertyData MaxHealthProperty = RegisterProperty(nameof(MaxHealth), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Максимальная толщина" петуха.
		/// </summary>
		public static readonly PropertyData MaxThicknessProperty = RegisterProperty(nameof(MaxThickness), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Максимальный вес" петуха.
		/// </summary>
		public static readonly PropertyData MaxWeightProperty = RegisterProperty(nameof(MaxWeight), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Максимальная юркость" петуха.
		/// </summary>
		public static readonly PropertyData MaxBricknessProperty = RegisterProperty(nameof(MaxBrickness), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Максимальная удача" петуха.
		/// </summary>
		public static readonly PropertyData MaxLuckProperty = RegisterProperty(nameof(MaxLuck), typeof(int));
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
			Color = RoosterColor.Black;
			Height = _minHeight;
		}

		/// <summary>
		/// Инициализует новый объект класса <see cref="RoosterModel" /> на основе существующего объекта <see cref="RoosterDto" />.
		/// </summary>
		public RoosterModel(RoosterDto rooster)
			: this()
		{
			if (rooster != null)
			{
				Token = rooster.Token;
				Stamina = rooster.Stamina;
				Brickness = rooster.Brickness;
				Luck = rooster.Luck;
				Thickness = rooster.Thickness;
				Color = ColorParse(rooster.Color);
				Crest = SizeParse(rooster.Crest);
				Height = rooster.Height;
				Weight = rooster.Weight;
				Name = rooster.Name;
				WinStreak = rooster.WinStreak;
				Health = rooster.Health;
				MaxHealth = rooster.MaxHealth;
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
		/// Возвращает или устанавливает токен.
		/// </summary>
		/// <value>
		/// Токен.
		/// </value>
		public string Token
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
			get => GetValue<int>(WinStreakProperty);
			set => SetValue(WinStreakProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает вес петуха.
		/// </summary>
		public double Weight
		{
			get => GetValue<double>(WeightProperty);
			set
			{
				SetValue(WeightProperty, value);
			}
		}
		/// <summary>
		/// Возвращает или устанавливает максимальный вес петуха.
		/// </summary>
		public int MaxWeight
		{
			get => GetValue<int>(MaxWeightProperty);
			set => SetValue(MaxWeightProperty, value);
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
				SetValue(HeightProperty, value);
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

			set => SetValue(BricknessProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает максимальную юркость петуха.
		/// </summary>
		public int MaxBrickness
		{
			get => GetValue<int>(MaxBricknessProperty);
			set => SetValue(MaxBricknessProperty, value);
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
			set => SetValue(ThicknessProperty, value); //Clamp(value, 0, _maxThickness));
		}
		/// <summary>
		/// Возвращает или устанавливает максимальную толщину петуха.
		/// </summary>
		public int MaxThickness
		{
			get => GetValue<int>(MaxThicknessProperty);
			set => SetValue(MaxThicknessProperty, value);
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
			set => SetValue(LuckProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает максимальную удачу петуха.
		/// </summary>
		public int MaxLuck
		{
			get => GetValue<int>(MaxLuckProperty);
			set => SetValue(MaxLuckProperty, value);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Выполняет преобразование объекта к типу RoosterDto.
		/// </summary>
		/// <returns></returns>
		public RoosterEditDto ToRoosterDto() =>
			new RoosterEditDto()
			{
				Height = Height,
				Color = ColorDtoParse(Color),
				Brickness = Brickness,
				Crest = SizeDtoParse(Crest),
				Weight = Weight,
				Luck = Luck,
				Name = Name,
				Thickness = Thickness
			};

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
				Token = Token
			};
		#endregion

		#region Private Methods

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

		private RoosterColor ColorParse(RoosterColorType color)
		{
			if (Enum.TryParse(color.ToString(), out RoosterColor outColor))
			{
				return outColor;
			}

			throw new ArgumentException();
		}

		private RoosterColorType ColorDtoParse(RoosterColor color)
		{
			if (Enum.TryParse(color.ToString(), out RoosterColorType outColor))
			{
				return outColor;
			}

			throw new ArgumentException();
		}

		private CrestSize SizeParse(CrestSizeType size)
		{
			if (Enum.TryParse(size.ToString(), out CrestSize outSize))
			{
				return outSize;
			}

			throw new ArgumentException();
		}

		private CrestSizeType SizeDtoParse(CrestSize size)
		{
			if (Enum.TryParse(size.ToString(), out CrestSizeType outSize))
			{
				return outSize;
			}

			throw new ArgumentException();
		}
		#endregion
	}
}
