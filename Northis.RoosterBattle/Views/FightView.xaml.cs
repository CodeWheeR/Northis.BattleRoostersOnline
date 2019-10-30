using System.Windows.Controls;

namespace Northis.BattleRoostersOnline.GameClient.Views
{
	/// <summary>
	/// Логика взаимодействия для FightView.xaml
	/// </summary>
	public partial class FightView
	{
		public FightView()
		{
			InitializeComponent();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			((TextBox) sender).ScrollToEnd();
		}
	}
}
