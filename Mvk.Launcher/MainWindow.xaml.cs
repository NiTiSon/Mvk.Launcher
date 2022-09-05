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
		Closed += (o, e) => { LauncherCore.SaveOptions(); };

		LauncherCore.OnVersionsRefreshed += VersionsUpdated;
		LauncherCore.OnOptionsLoaded += OptionsLoaded;

		InitializeComponent();
		LauncherCore.LoadVersions();
		LauncherCore.LoadOptions();
	}

	private void VersionsUpdated()
	{

	}
	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);

		this.optionPlayerName.TextChanged -= UpdateName;
	}
	private void OptionsLoaded()
	{
		this.optionPlayerName.Text = LauncherCore.Options.PlayerName;
		this.optionPlayerName.TextChanged += UpdateName;
	}
	private void UpdateName(object sender, TextChangedEventArgs e)
	{
		LauncherCore.Options.PlayerName = String.IsNullOrWhiteSpace(this.optionPlayerName.Text) ? "Player" : this.optionPlayerName.Text;
	}

	public void DragWindow(object sender, MouseButtonEventArgs args)
	{
		DragMove();
	}
	public void ExitClicked(object sender, RoutedEventArgs args)
	{
		this.Close();
	}

	public void VersionNameLoad(object sender, RoutedEventArgs e)
	{
		if (sender is TextBlock tb)
		{
			tb.Text = "Version: " + LauncherCore.ApplicationVersion;
		}
	}
	public void NiTiSLinkClick(object sender, EventArgs args)
		=> LauncherCore.OpenBrowser("https://github.com/NiTiS-Dev");
	public void PlayButtonClicked(object sender, RoutedEventArgs e)
	{
		LauncherCore.ShowError("Version not selected");
	}
	public void VersionSelectLoaded(object sender, RoutedEventArgs e)
	{
	}
}
