using System;
using System.Globalization;
using Catel.MVVM.Converters;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует вещественное значение в текстовое описание ограничения.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class MaxDoubleValueToTextLimiter : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует вещественное значение в текстовое описание ограничения.
		/// </summary>
		/// <param name="value">Вещественный ограничитель.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => $"(2.0 - {(int) value}.0)";

		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Вещественный ограничитель.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Метод не имеет реализации.</returns>
		/// <exception cref="NotImplementedException">Метод не имеет реализации.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
