using Mvk.Launcher;
using Mvk.Launcher.GameAdapter;
using Mvk.Launcher.GameAdapter.v0;
using System;
using System.IO;
using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for LaunchWindow.xaml
/// </summary>
public partial class NewInstanceWindow : Window
{
	public MvkGameInstance? createdInstance; 
	public NewInstanceWindow() : base()
	{
		InitializeComponent();

		this.versionBox.Items.Clear();
		foreach (MvkVersion version in LauncherCore.GameVersions)
		{
			MvkComboBoxItem comboBoxItem = new(version.Version.ToString(), version.DownloadURI);
			this.versionBox.Items.Add(comboBoxItem);
		}
	}

	public void ExitClicked(object sender, RoutedEventArgs args)
	{
		this.Close();
	}

	private void DenyClick(object sender, RoutedEventArgs e)
	{
		createdInstance = null;

		ExitClicked(this, new RoutedEventArgs());
    }
	private void AddClick(object sender, RoutedEventArgs e)
	{
		MvkGameInstance instance = new();

		createdInstance = instance;

		createdInstance.Version = (this.versionBox.SelectedItem as MvkComboBoxItem)!.Content as string;

		createdInstance.Name = this.instName.Text;

		createdInstance.Favorite = false;

		createdInstance.SaveLocation = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), ".mvk");

		ExitClicked(this, new RoutedEventArgs());
	}

	private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		DragMove();
	}
}
