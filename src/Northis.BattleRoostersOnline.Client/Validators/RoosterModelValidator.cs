using System.Collections.Generic;
using Catel.Data;
using Northis.BattleRoostersOnline.Client.Models;

namespace Northis.BattleRoostersOnline.Client.Validators
{
    /// <summary>
    /// Класс, предоставляющий проверку вводимых пользователем значений.
    /// </summary>
    /// <seealso cref="ValidatorBase{Northis.BattleRoostersOnline.Client.Models.RoosterModel}" />
    internal class RoosterModelValidator : ValidatorBase<RoosterModel>
	{
		#region Protected Methods
		/// <summary>
		/// Осуществляет валидацию полей объекта. Результаты должны быть добавлены в лист валидации.
		/// results.
		/// </summary>
		/// <param name="instance">Объект проверки.</param>
		/// <param name="validationResults">Результаты валидации.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="validationResults" /> is <c>null</c>.</exception>
		/// <remarks>
		/// Нет необходимости проверять аргументы. Они уже проверены на правильность в
		/// <see cref="T:Catel.Data.ValidatorBase`1" />.
		/// </remarks>
		protected override void ValidateFields(RoosterModel instance, List<IFieldValidationResult> validationResults)
		{
			if (string.IsNullOrWhiteSpace(instance.Name) || instance.Name.Length > 15)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.NameProperty, "Rooster name is required"));
			}

			if (instance.Brickness < 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.BricknessProperty, "Brickness value mustn't be negative"));
			}

			if (instance.Weight <= 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.WeightProperty, "Weight value is required"));
			}

			if (instance.Height <= 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.HeightProperty, "Height value is required"));
			}

			if (instance.Luck < 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.LuckProperty, "Luck value mustn't be negative"));
			}

			if (instance.Thickness < 0)
			{
				validationResults.Add(FieldValidationResult.CreateError(RoosterModel.ThicknessProperty, "Thickness value mustn't be negative"));
			}
		}
		#endregion
	}
}
