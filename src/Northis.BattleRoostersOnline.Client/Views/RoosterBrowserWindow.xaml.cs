using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Northis.BattleRoostersOnline.Client.Views
{
	/// <summary>
	/// Логика взаимодействия для RoosterBrowserWindow.xaml
	/// </summary>
	public partial class RoosterBrowserWindow
	{
		public RoosterBrowserWindow()
		{
			InitializeComponent();
		}

		private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var descriptor = ((PropertyDescriptor) e.PropertyDescriptor);
			if (!string.IsNullOrWhiteSpace(descriptor.DisplayName))
				e.Column.Header = descriptor.DisplayName;
		}

		private void DataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
		{
			MessageBox.Show("Asdasd");
		}
	}
}
