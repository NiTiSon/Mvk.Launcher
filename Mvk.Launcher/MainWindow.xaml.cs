using Mvk.Launcher;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		//this.versionLabel.Content = $"Version: {LauncherCore.ApplicationVersion}";
	}
	public void DragWindow(object sender, MouseButtonEventArgs args)
	{
		DragMove();
	}
	public void ExitClicked(object sender, RoutedEventArgs args)
	{
		this.Close();
	}

	private void VersionNameLoad(object sender, RoutedEventArgs e)
	{
		if (sender is TextBlock tb)
		{
			tb.Text = "Version: " + LauncherCore.ApplicationVersion;
		}
	}
	public void NiTiSLinkClick(object sender, EventArgs args)
	{
		using Process process = new();
		process.StartInfo.FileName = "explorer.exe";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.ArgumentList.Add("https://github.com/NiTiS-Dev");

		process.Start();
	}
	public void PlayButtonClicked(object sender, RoutedEventArgs e)
	{
		LauncherCore.ShowError("Version not selected");
	}
}
