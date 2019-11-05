using System.Windows.Controls;

namespace Northis.BattleRoostersOnline.Client.Views
{
    /// <summary>
    /// Обеспечивает логику взаимодействия с FightView.xaml.
    /// </summary>
    public partial class FightView
	{
        #region .ctor
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="FightView"/> класса.
        /// </summary>
        public FightView()
		{
			InitializeComponent();
		}
        #endregion

        #region Private Methods
        /// <summary>
        /// Обрабатывает событие изменения текста в TextBox.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e"> <see cref="TextChangedEventArgs"/> Экземпляр, содержащий информацию о событии.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			((TextBox) sender).ScrollToEnd();
		}
        #endregion
    }
}
