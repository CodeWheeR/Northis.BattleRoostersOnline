using System;
using System.Globalization;
using Catel.MVVM.Converters;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует целочисленное значение в текстовое описание ограничения.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class MaxValueToTextLimiter : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует целочисленное значение в текстовое описание ограничения.
		/// </summary>
		/// <param name="value">Целочисленное значение.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => $"(0 - {(int)value})";
		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Целочисленное значение.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
