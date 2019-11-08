using System;
using System.Globalization;
using Catel.MVVM.Converters;
using Northis.BattleRoostersOnline.Client.Models;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует значение здоровья в bool значение жив/мертв.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class HeathValueToBool : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует значение здоровья в bool значение жив/мертв.
		/// </summary>
		/// <param name="value">Значение здоровья.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var answer = false;
			answer = (RoosterModel) value != null && (value as RoosterModel).Health == 0;
			return answer;
		}
		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Значение здоровья.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Метод не имеет реализации.</returns>
		/// <exception cref="NotImplementedException">Метод не имеет реализации.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
