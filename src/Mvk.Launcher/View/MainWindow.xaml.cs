using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using Mvk.Launcher.ViewModel;

namespace Mvk.Launcher;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainWindowViewModel();
	}

	private void ScaleButton_Switch(object sender, RoutedEventArgs e)
	{
		if (WindowState is WindowState.Maximized)
		{
			WindowState = WindowState.Normal;
			root.Margin = new(0);
		}
		else
		{
			WindowState = WindowState.Maximized;
			root.Margin = new(8);
		}
	}

	private void MinimizeButton_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized;
	}

	private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.GetPosition(this).Y <= 35) // Header size, TODO?: Move to the theme values
			DragMove();
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		WindowChrome.SetWindowChrome(this, new()
		{
			GlassFrameThickness = new(0),
			CornerRadius = new(2, 2, 2, 2),
			CaptionHeight = 0,
			UseAeroCaptionButtons = false,
			ResizeBorderThickness = new(6),
		});
	}

	private void CloseButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}
