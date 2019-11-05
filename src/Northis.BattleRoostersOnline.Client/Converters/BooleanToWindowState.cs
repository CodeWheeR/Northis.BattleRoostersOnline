using System;
using System.Globalization;
using System.Windows;
using Catel.MVVM.Converters;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует значение перечисления RoosterColor в путь к изображениям петухов.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class BooleanToWindowState : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует значение Bool в состояние окна.
		/// </summary>
		/// <param name="value">Значение перечисления RoosterColor.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value as bool? == true) ? WindowState.Normal : WindowState.Minimized;
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
