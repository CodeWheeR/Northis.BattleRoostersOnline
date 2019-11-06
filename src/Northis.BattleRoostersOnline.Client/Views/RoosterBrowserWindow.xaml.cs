using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Northis.BattleRoostersOnline.Client.Views
{
    /// <summary>
    /// Обеспечивает логику взаимодействия с RoosterBrowserWindow.xaml.
    /// </summary>
    public partial class RoosterBrowserWindow
	{
        #region .ctor
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoosterBrowserWindow"/> класса.
        /// </summary>
        public RoosterBrowserWindow()
		{
			InitializeComponent();
		}
        #endregion

        #region Private Methods        
        /// <summary>
        /// Обрабатывает событие автогенерации столбцов DataGrid.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e"><see cref="DataGridAutoGeneratingColumnEventArgs"/> Экземпляр, содержащий информацию о событии.</param>
        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var descriptor = ((PropertyDescriptor) e.PropertyDescriptor);
			if (!string.IsNullOrWhiteSpace(descriptor.DisplayName))
				e.Column.Header = descriptor.DisplayName;
		}
		#endregion
    }
}
