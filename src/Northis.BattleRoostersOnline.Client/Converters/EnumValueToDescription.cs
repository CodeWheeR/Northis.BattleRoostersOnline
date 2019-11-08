using System;
using System.Globalization;
using Catel.MVVM.Converters;
using Northis.BattleRoostersOnline.Client.Extensions;

namespace Northis.BattleRoostersOnline.Client.Converters
{
	/// <summary>
	/// Конвертирует значение Enum, помеченного аттрибутом Display, в путь к картинке.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class EnumValueToDescription : IValueConverter
	{
		#region Public Methods
		/// <summary>
		/// Конвертирует значение Enum, помеченного аттрибутом Display, в путь к картинке.
		/// </summary>
		/// <param name="value">Значение перечисления.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((Enum) value).GetDisplayFromResource();
		/// <summary>
		/// Метод не имеет реализации.
		/// </summary>
		/// <param name="value">Значение перечисления.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Метод не имеет реализации.</returns>
		/// <exception cref="NotImplementedException">Метод не имеет реализации.</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}
