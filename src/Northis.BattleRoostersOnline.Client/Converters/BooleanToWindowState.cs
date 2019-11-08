using System;
using System.Globalization;
using System.Windows;
using Catel.MVVM.Converters;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует значение bool в состояние окна.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class BooleanToWindowState : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует значение Bool в состояние окна.
		/// </summary>
		/// <param name="value">bool-значение.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value as bool? == true ? WindowState.Normal : WindowState.Minimized;

		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Значение.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Метод не имеет реализации.</returns>
		/// <exception cref="NotImplementedException">Метод не имеет реализации.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
