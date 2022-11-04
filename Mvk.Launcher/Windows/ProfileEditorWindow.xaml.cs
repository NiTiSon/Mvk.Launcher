using Mvk.Launcher.Core;
using Serilog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mvk.Launcher.App.Windows;
/// <summary>
/// Interaction logic for ProfileEditorWindow.xaml
/// </summary>
public partial class ProfileEditorWindow : Window
{
	public Profile? Profile { get; private set; }
	private readonly ComboBoxItem? item;
	public ProfileEditorWindow(ComboBoxItem? item)
	{
		Log.Information("{0}.new()", nameof(ProfileEditorWindow));
		this.item = item;

		if (item is not null)
			this.createOrSaveButton.Content = "Save";

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
