using System;
using System.Globalization;
using Catel.MVVM.Converters;
using Northis.RoosterBattle.Models;

namespace Northis.RoosterBattle.Converters
{
	/// <summary>
	/// Конвертирует значение перечисления RoosterColor в путь к изображениям петухов.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class HeathValueToBool : IValueConverter
	{
		/// <summary>
		/// Конвертирует значение перечисления в путь к картинке.
		/// </summary>
		/// <param name="value">Значение перечисления RoosterColor.</param>
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
		/// <param name="value">Значение перечисления RoosterColor.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
