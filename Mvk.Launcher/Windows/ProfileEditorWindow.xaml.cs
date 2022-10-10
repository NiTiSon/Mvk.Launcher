using Serilog;
using System.Windows;
using System.Windows.Input;

namespace Mvk.Launcher.App.Windows;
/// <summary>
/// Interaction logic for ProfileEditorWindow.xaml
/// </summary>
public partial class ProfileEditorWindow : Window
{
	public ProfileEditorWindow()
	{
		Log.Information("{0}.new()", nameof(ProfileEditorWindow));
		InitializeComponent();
	}
	public void DragWindow(object sender, MouseButtonEventArgs args)
		=> DragMove();
	public void ExitClicked(object sender, RoutedEventArgs args)
		=> Close();
	public void SaveClicked(object sender, RoutedEventArgs args)
	{


		Close();
	}
}
