using System;
using System.Globalization;
using Catel.MVVM.Converters;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует значение перечисления RoosterColor в путь к изображениям петухов.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class MaxDoubleValue : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует значение Enum, помеченного аттрибутом Display, в путь к картинке.
		/// </summary>
		/// <param name="value">Значение перечисления RoosterColor.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => $"(0.0 - {(int)value}.0)";
		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Значение перечисления RoosterColor.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
