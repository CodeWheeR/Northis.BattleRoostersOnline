﻿using System;
using System.Collections.Generic;
using System.Linq;
using Northis.BattleRoostersOnline.Dto;

namespace Northis.BattleRoostersOnline.Service.Models
{
	/// <summary>
	/// Предоставляет модель петуха.
	/// </summary>
	/// <seealso cref="ValidatableModelBase" />
	public class RoosterModel : ICloneable
	{
		#region Fields		
		/// <summary>
		/// Вес петуха.
		/// </summary>
		private double _weight;
		/// <summary>
		/// Рост петуха.
		/// </summary>
		private int _height;
		/// <summary>
		/// Здоровье петуха.
		/// </summary>
		private double _health;
		/// <summary>
		/// Выносливость петуха.
		/// </summary>
		private int _stamina;
		/// <summary>
		/// Цвет петуха.
		/// </summary>
		private Dto.RoosterColor _color;
		/// <summary>
		/// Уклонеине петуха.
		/// </summary>
		private int _brickness;
		/// <summary>
		/// Удачливость петуха.
		/// </summary>
		private int _luck;
		/// <summary>
		/// Плотность петуха.
		/// </summary>
		private int _thickness;
		/// <summary>
		/// Генератор удачливости петуха.
		/// </summary>
		private readonly Random _luckMeter = new Random();
		/// <summary>
		/// Генератор уклонения петуха.
		/// </summary>
		private readonly Random _bricknessMeter = new Random();
		/// <summary>
		/// Словарь с делегатами цветовых модификаций петухов.
		/// </summary>
		private readonly Dictionary<Dto.RoosterColor, Action> ColorModifications;
		/// <summary>
		/// Словарь с делегатами очистки цветовых модификаций петухов.
		/// </summary>
		private readonly Dictionary<Dto.RoosterColor, Action> ClearModifications;

		#region Limiters		
		/// <summary>
		/// Минимальный вес петуха.
		/// </summary>
		private readonly int _minWeight = 2;
		/// <summary>
		/// Максимальный вес петуха.
		/// </summary>
		private int _maxWeight = 8;
		/// <summary>
		/// Максимальная высота петуха.
		/// </summary>
		private readonly int _maxHeight = 50;
		/// <summary>
		/// Минимальная высота петуха.
		/// </summary>
		private readonly int _minHeight = 20;
		/// <summary>
		/// Максимальное здоровье петуха.
		/// </summary>
		private int _maxHealth = 100;
		/// <summary>
		/// Максимальная выносливость петуха.
		/// </summary>
		private readonly int _maxStamina = 100;
		/// <summary>
		/// Максимальная плотность петуха.
		/// </summary>
		private int _maxThickness = 30;
		/// <summary>
		/// Максимальная удачливость петуха.
		/// </summary>
		private int _maxLuck = 30;
		/// <summary>
		/// Максимальное уклонение петуха.
		/// </summary>
		private int _maxBrickness = 30;
		#endregion

		#region Consts		
		/// <summary>
		/// Максимальное здоровье петуха по умолчанию.
		/// </summary>
		private const int DefaultMaxHealth = 100;
		/// <summary>
		/// Максимальный вес петуха по умолчанию.
		/// </summary>
		private const int DefaultMaxWeight = 8;
		/// <summary>
		/// Максимальная плотность петуха по умолчанию.
		/// </summary>
		private const int DefaultMaxThickness = 30;
		/// <summary>
		/// Максимальное уклонение петуха по умолчанию.
		/// </summary>
		private const int DefaultMaxBrickness = 30;
		/// <summary>
		/// Максимальная удачливость петуха по умолчанию.
		/// </summary>
		private const int DefaultMaxLuck = 30;
        #endregion
        #endregion

        #region .ctor
        /// <summary>
        /// Инициализует новый объект <see cref="RoosterModel" /> класса.
        /// </summary>
        public RoosterModel()
		{
			Health = 100;
			MaxHealth = 100;
			Stamina = 100;

			ColorModifications = new Dictionary<Dto.RoosterColor, Action>
			{
				{
					Dto.RoosterColor.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, 120);
						MaxHealth = _maxHealth;
						Health = _maxHealth;
					}
				},
				{
					Dto.RoosterColor.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, 50)
				},
				{
					Dto.RoosterColor.Black, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, 10)
				},
				{
					Dto.RoosterColor.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, 50)
				},
				{
					Dto.RoosterColor.White, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, 50)
				}
			};

			ClearModifications = new Dictionary<Dto.RoosterColor, Action>
			{
				{
					Dto.RoosterColor.Red, () =>
					{
						ChangeMaxLimit(nameof(Health), 0, ref _maxHealth, DefaultMaxHealth);
						MaxHealth = DefaultMaxHealth;
					}
				},
				{
					Dto.RoosterColor.Blue, () => ChangeMaxLimit(nameof(Brickness), 0, ref _maxBrickness, DefaultMaxBrickness)
				},
				{
					Dto.RoosterColor.White, () => ChangeMaxLimit(nameof(Luck), 0, ref _maxLuck, DefaultMaxLuck)
				},
				{
					Dto.RoosterColor.Brown, () => ChangeMaxLimit(nameof(Thickness), 0, ref _maxThickness, DefaultMaxThickness)
				},
				{
					Dto.RoosterColor.Black, () => ChangeMaxLimit(nameof(Weight), _minWeight, ref _maxWeight, DefaultMaxWeight)
				}
			};
		}

		/// <summary>
		/// Инициализирует новый объект <see cref="RoosterModel" /> класса.
		/// </summary>
		/// <param name="rooster">Петух.</param>
		public RoosterModel(RoosterDto rooster)
			: this()
		{
			Health = rooster.Health;
			MaxHealth = rooster.MaxHealth;
			Stamina = rooster.Stamina;
			Brickness = rooster.Brickness;
			Luck = rooster.Luck;
			Thickness = rooster.Thickness;
			Color = rooster.Color;
			Crest = rooster.Crest;
			Height = rooster.Height;
			Weight = rooster.Weight;
			Name = rooster.Name;
			WinStreak = rooster.WinStreak;
		}

		/// <summary>
		/// Инициализирует новый объект <see cref="RoosterModel" /> класса.
		/// </summary>
		/// <param name="rooster">Петух.</param>
		public RoosterModel(RoosterEditDto rooster)
			: this()
		{
			Brickness = rooster.Brickness;
			Luck = rooster.Luck;
			Thickness = rooster.Thickness;
			Color = rooster.Color;
			Crest = rooster.Crest;
			Height = rooster.Height;
			Weight = rooster.Weight;
			if (rooster.Name.Length > 15)
			{
				Name = rooster.Name.Substring(0,15);
			}
			else
			{
				Name = rooster.Name;
			}
		}
        #endregion

        #region Properties		        
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
		/// Максимальное здоровье петуха.
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
		public Dto.RoosterColor Color
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
		public Dto.CrestSize Crest
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
				if (luck < Luck)
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
		/// Конвертирует объект RoosterModel в объект RoosterDto.
		/// </summary>
		/// <returns>RoosterDto.</returns>
		public RoosterDto ToRoosterDto() =>
			new RoosterDto
			{
				Height = Height,
				Color = Color,
				Health = Health,
				Stamina = Stamina,
				Brickness = Brickness,
				Crest = Crest,
				Weight = Weight,
				WinStreak = WinStreak,
				Luck = Luck,
				Name = Name,
				Thickness = Thickness,
				MaxHealth = MaxHealth
			};

		/// <summary>
		/// Принимает удар от другого петуха.
		/// </summary>
		/// <param name="sender">Ударивший петух.</param>
		public double TakeHit(RoosterModel sender)
		{
			var damage = 0.0;
			if (_bricknessMeter.Next(0, 100) >= Brickness || Stamina == 0)
			{
				damage = sender.Hit * (1 - (double) Thickness / 100);
				Health -= damage;
				Health = Math.Round(Health, 2);
			}

			Stamina -= 5;
			return Math.Round(damage, 2);
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
		/// <param name="value">Значение.</param>
		/// <param name="min">Минимум.</param>
		/// <param name="max">Максимум.</param>
		/// <returns>T.</returns>
		private T Clamp<T>(T value, T min, T max) where T : IComparable => value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;

		/// <summary>
		/// Вычисляет порядковый индекс значения перечисления.
		/// </summary>
		/// <param name="first">Обыъект перечисления.</param>
		/// <returns>Индекс.</returns>
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
		/// <param name="propertyName">Имя свойства.</param>
		/// <param name="minValue">Минимум.</param>
		/// <param name="maxValue">Максимум.</param>
		/// <param name="newValue">Новое значение.</param>
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
