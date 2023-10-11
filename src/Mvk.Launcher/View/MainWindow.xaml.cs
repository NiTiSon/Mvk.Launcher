using System.Windows;
using System.Windows.Shell;
using Mvk.Launcher.ViewModel;

namespace Mvk.Launcher;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainWindowViewModel();
	}

	private void CloseButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}
	private void ScaleButton_Switch(object sender, RoutedEventArgs e)
	{
		if (WindowState is WindowState.Maximized)
			WindowState = WindowState.Normal;
		else
			WindowState = WindowState.Maximized;
	}

	private void MinimizeButton_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized;
	}
}
