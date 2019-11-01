using System.Collections.ObjectModel;
using Catel.Data;

namespace Northis.BattleRoostersOnline.Client.Models
{
	/// <summary>
	/// Отвечает за сохранение и загрузку конфигураций петухов.
	/// </summary>
	/// <seealso cref="Catel.Data.SavableModelBase{Northis.RoosterBattle.Models.RoosterSettings}" />
	internal class RoosterSettings : SavableModelBase<RoosterSettings>
	{
		/// <summary>
		/// Возвращает или устанавливает коллекцию петухов для сохранения/загрузки.
		/// </summary>
		public ObservableCollection<RoosterModel> Roosters
		{
			get => GetValue<ObservableCollection<RoosterModel>>(RoostersProperty);
			set => SetValue(RoostersProperty, value);
		}
		/// <summary>
		/// Зарегистрированное свойство петухи.
		/// </summary>
		public static readonly PropertyData RoostersProperty =
			RegisterProperty("Roosters", typeof(ObservableCollection<RoosterModel>), () => new ObservableCollection<RoosterModel>());
	}
}
