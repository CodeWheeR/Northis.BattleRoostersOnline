using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Northis.BattleRoostersOnline.GameClient.UserControls
{
	/// <summary>
	/// Логика взаимодействия для ValueBar.xaml
	/// </summary>
	public partial class ValueBar : UserControl
	{
		#region Fields
		private double _lastObjectSize;

		#region Static
		public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(nameof(ValueLevel),
																							  typeof(double),
																							  typeof(ValueBar),
																							  new FrameworkPropertyMetadata(
																								  0.0,
																								  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
																								  OnValueLevelChanged));

		public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register(nameof(TextSize),
																								 typeof(double),
																								 typeof(ValueBar),
																								 new FrameworkPropertyMetadata(
																									 16.0,
																									 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
																									 OnTextSizeChanged));

		public static readonly DependencyProperty ValueColorProperty = DependencyProperty.Register(nameof(ValueColor),
																								   typeof(Brush),
																								   typeof(ValueBar),
																								   new FrameworkPropertyMetadata(
																									   Brushes.DarkRed,
																									   FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
																									   OnValueColorChanged));

		public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
																								 typeof(int),
																								 typeof(ValueBar),
																								 new FrameworkPropertyMetadata(
																									 100,
																									 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
																									 OnMaxValueChanged));
		#endregion
		#endregion

		#region Properties
		public ValueBar()
		{
			InitializeComponent();
		}

		#region Properties		
		/// <summary>
		/// Возвращает или устанавливает значение размера шрифта выводимого значения.
		/// </summary>
		public double TextSize
		{
			get => (double) GetValue(TextSizeProperty);
			set => SetValue(TextSizeProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает фоновый цвет элемента.
		/// </summary>
		public Brush ValueColor
		{
			get => (Brush) GetValue(ValueColorProperty);
			set => SetValue(ValueColorProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает значение элемента.
		/// </summary>
		public double ValueLevel
		{
			get => (double) GetValue(LevelProperty);
			set => SetValue(LevelProperty, value);
		}

		/// <summary>
		/// Возвращает или устанавливает максимально возможное значение элемента.
		/// </summary>
		public int MaxValue
		{
			get => (int) GetValue(MaxValueProperty);
			set => SetValue(MaxValueProperty, value);
		}
		#endregion
		#endregion

		#region Private Methods
		#region Static
		private static void OnTextSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var obj = d as ValueBar;
			obj.Value.FontSize = (double) e.NewValue;
		}

		private static void OnValueColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var obj = d as ValueBar;
			obj.Level.Background = (Brush) e.NewValue;
		}

		private static void OnValueLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var obj = d as ValueBar;
			ReloadValueBar(obj, obj.MaxValue, (double) e.NewValue);
			obj.Value.Text = Math.Round((double) e.NewValue)
								 .ToString(CultureInfo.CurrentCulture);
		}

		private static void ReloadValueBar(ValueBar obj, int maxValue, double newValue)
		{
			obj.Level.Margin = new Thickness(0, 0, obj.ActualWidth / 100 * (100 - (newValue != 0 ? newValue / maxValue * 100 : 0)), 0);
		}

		private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ReloadValueBar(d as ValueBar, (int) e.NewValue, (d as ValueBar).ValueLevel);
		}
		#endregion

		private void Level_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (Math.Abs(_lastObjectSize - ActualWidth) > 0.01)
			{
				Level.Margin = new Thickness(0, 0, ActualWidth / 100 * (100 - (ValueLevel != 0 ? ValueLevel / MaxValue * 100 : 0)), 0);
				_lastObjectSize = ActualWidth;
			}
		}
		#endregion
	}
}
