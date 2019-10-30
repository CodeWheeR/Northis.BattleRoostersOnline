using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Catel.MVVM.Converters;
using Northis.BattleRoostersOnline.GameClient.Extensions;
using Northis.BattleRoostersOnline.GameClient.Models;

namespace Northis.BattleRoostersOnline.GameClient.Converters
{
	/// <summary>
	/// Конвертирует значение перечисления RoosterColor в описание модификации.
	/// </summary>
	/// <seealso cref="Catel.MVVM.Converters.IValueConverter" />
	public class ColorToDescriptionConverter : IValueConverter
	{
		Dictionary<RoosterColor, string> _colorDescriptions = new Dictionary<RoosterColor, string>()
		{
			{
				RoosterColor.Black, "Максимальный вес = 10"
			},
			{
				RoosterColor.Blue, "Максимальная Юркость = 50"
			},
			{
				RoosterColor.Brown, "Максимальная толщина покрова = 50"
			},
			{
				RoosterColor.Red, "Максимальное здоровье = 120"
			},
			{
				RoosterColor.White, "Максимальная удача = 50"
			}
		};


		/// <summary>
		/// Конвертирует значение Color в описание модификации.
		/// </summary>
		/// <param name="value">Значение перечисления RoosterColor.</param>
		/// <param name="targetType">Целевой тип конвертации.</param>
		/// <param name="parameter">Параметр.</param>
		/// <param name="culture">Региональные настройки и параметры.</param>
		/// <returns>Строковый путь к изображению.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (value is RoosterColor rc)  ? _colorDescriptions[rc] : "";

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
