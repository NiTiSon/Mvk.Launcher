using System.Windows;
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
		}
		else
		{
			WindowState = WindowState.Maximized;
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

	private void CloseButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
}
