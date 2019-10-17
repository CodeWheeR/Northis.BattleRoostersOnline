using System;
using System.Collections.Generic;
using Catel.Data;
using Catel.MVVM;
using Northis.RoosterBattle.Models;

namespace Northis.RoosterBattle.ViewModels
{
	/// <summary>
	/// Обеспечивает взаимодействие модели RoosterModel и представления EditRooster.
	/// </summary>
	/// <seealso cref="Catel.MVVM.ViewModelBase" />
	//[ValidateModel(typeof(Validators.RoosterModelValidator))]
	class EditRoosterViewModel : ViewModelBase
	{
		#region Fields

		#region Static
		/// <summary>
		/// Зарегистрированное свойство модель петуха, помеченное атрибутом [Model].
		/// </summary>
		public static readonly PropertyData RoosterModelProperty = RegisterProperty(nameof(RoosterModel), typeof(RoosterModel), null);
		/// <summary>
		/// Зарегистрированное свойство "Вес" петуха.
		/// </summary>
		public static readonly PropertyData WeightProperty = RegisterProperty(nameof(Weight), typeof(double));
		/// <summary>
		/// Зарегистрированное свойство "Высота" петуха.
		/// </summary>
		public static readonly PropertyData HeightProperty = RegisterProperty(nameof(Height), typeof(int));
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
		/// Зарегистрированное свойство "Гребень" петуха.
		/// </summary>
		public static readonly PropertyData CrestProperty = RegisterProperty(nameof(Crest), typeof(CrestSize));
		/// <summary>
		/// Зарегистрированное свойство "Плотность" петуха.
		/// </summary>
		public static readonly PropertyData ThicknessProperty = RegisterProperty(nameof(Thickness), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Удача" петуха.
		/// </summary>
		public static readonly PropertyData LuckProperty = RegisterProperty(nameof(Luck), typeof(int));
		/// <summary>
		/// Зарегистрированное свойство "Имя" петуха.
		/// </summary>
		public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string));
		/// <summary>
		/// Зарегистрированное свойство "Серия побед" петуха.
		/// </summary>
		public static readonly PropertyData WinStreakProperty = RegisterProperty(nameof(WinStreak), typeof(int));
		#endregion

		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый объект <see cref="EditRoosterViewModel"/> класса.
		/// </summary>
		/// <param name="selectedRooster">Выбранный петух.</param>
		public EditRoosterViewModel(RoosterModel selectedRooster)
		{
			RoosterModel = selectedRooster;
			ColorsArray = Enum.GetValues((typeof(RoosterColor)));
			CrestsArray = Enum.GetValues(typeof(CrestSize));
		}
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает или устанавливает типы окраски петуха.
		/// </summary>
		/// <value>
		/// Окраски петуха.
		/// </value>
		public Array ColorsArray
		{
			get;
		}
		/// <summary>
		/// Возвращает или устанавливает типы гребней петуха.
		/// </summary>
		/// <value>
		/// Типы гребней петуха.
		/// </value>
		public Array CrestsArray
		{
			get;
		}

		/// <summary>
		/// Возвращает или устанавливает количество побед петуха.
		/// </summary>
		/// <value>
		/// Количество побед.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int WinStreak
		{
			get => GetValue<int>(WinStreakProperty);
			set => SetValue(WinStreakProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает имя петуха.
		/// </summary>
		/// <value>
		/// Имя петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public string Name
		{
			get => GetValue<string>(NameProperty);
			set => SetValue(NameProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает удачливость петуха.
		/// </summary>
		/// <value>
		/// Удачливость петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int Luck
		{
			get => GetValue<int>(LuckProperty);
			set => SetValue(LuckProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает плотность петуха.
		/// </summary>
		/// <value>
		/// Плотность петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int Thickness
		{
			get => GetValue<int>(ThicknessProperty);
			set => SetValue(ThicknessProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает броню петуха.
		/// </summary>
		/// <value>
		/// Броня петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public CrestSize Crest
		{
			get => GetValue<CrestSize>(CrestProperty);
			set => SetValue(CrestProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает юркость петуха.
		/// </summary>
		/// <value>
		/// Юркость петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int Brickness
		{
			get => GetValue<int>(BricknessProperty);
			set => SetValue(BricknessProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает окрас петуха.
		/// </summary>
		/// <value>
		/// Окрас петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public RoosterColor Color
		{
			get => GetValue<RoosterColor>(ColorProperty);
			set => SetValue(ColorProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает выносливость петуха.
		/// </summary>
		/// <value>
		/// Выносливость петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int Stamina
		{
			get => GetValue<int>(StaminaProperty);
			set => SetValue(StaminaProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает высоту петуха.
		/// </summary>
		/// <value>
		/// Высота петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public int Height
		{
			get => GetValue<int>(HeightProperty);
			set => SetValue(HeightProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает модель петуха.
		/// </summary>
		/// <value>
		/// Модель петуха.
		/// </value>
		[Model]
		public RoosterModel RoosterModel
		{
			get => GetValue<RoosterModel>(RoosterModelProperty);
			set => SetValue(RoosterModelProperty, value);
		}
		/// <summary>
		/// Возвращает или устанавливает вес петуха.
		/// </summary>
		/// <value>
		/// Вес петуха.
		/// </value>
		[ViewModelToModel("RoosterModel")]
		public double Weight
		{
			get => GetValue<double>(WeightProperty);
			set => SetValue(WeightProperty, value);
		}
		#endregion

		/// <summary>
		/// Validates the field values of this object. Override this method to enable
		/// validation of field values.
		/// </summary>
		/// <param name="validationResults">The validation results, add additional results to this list.</param>
		protected override void ValidateFields(List<IFieldValidationResult> validationResults)
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.NameProperty, "Rooster name is required"));
			}

			if (Brickness < 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.BricknessProperty, "Brickness value mustn't be negative"));
			}

			if (Weight <= 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.WeightProperty, "Weight value is required"));
			}

			if (Height <= 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.HeightProperty, "Height value is required"));
			}

			if (Luck < 0)
			{ 
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.LuckProperty, "Luck value mustn't be negative"));
			}

			if (Thickness < 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.ThicknessProperty, "Thickness value mustn't be negative"));
			}
		}

	}
}
