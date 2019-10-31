using System.Collections.Generic;
using Catel.Data;
using Northis.BattleRoostersOnline.Client.Models;

namespace Northis.BattleRoostersOnline.Client.Validators
{
	internal class RoosterModelValidator : ValidatorBase<RoosterModel>
	{
		/// <summary>
		/// Validates the fields of the specified instance. The results must be added to the list of validation
		/// results.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <param name="validationResults">The validation results.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> is <c>null</c>.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="validationResults" /> is <c>null</c>.</exception>
		/// <remarks>
		/// There is no need to check for the arguments, they are already ensured to be correct in the
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
	}
}
