using Mvk.Launcher;
using Mvk.Launcher.App.Windows;
using Mvk.Launcher.Core;
using NiTiS.IO;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Mkv.Launcher.Windows;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LauncherWindow
{
	internal readonly LauncherCore core;
	public LauncherWindow(Directory saveDirectory)
	{
		Log.Information("{0}.new()", nameof(LauncherWindow));
		DataContext = this;
		core = new(saveDirectory);
		core.LoadOptionsFile().Wait();

		bool _vLoaded = false;
		Task loadV = core.LoadVersions();
		loadV.OnComplete(() =>
		{
			_vLoaded = true;
		});
		Task wait = Task.Delay(1000 * 15).OnComplete(() =>
		{
			if (!_vLoaded)
			{
				Log.Error("{0} takes too long to execute", nameof(LauncherCore.LoadVersions));
			}
		});
		Task.WaitAny(loadV, wait);

		InitializeComponent();

		if (_vLoaded)
		{
			newGameInstanceButton.IsEnabled = true;
			playButton.IsEnabled = false;
		}
	}
	#region WindowFields
	public string PlayerNameBind
	{
		get => core.Options.UserName ?? "Player";
		set => core.Options.UserName = value;
	}
	public string VersionBind
		=> App.Version.ToString();
	#endregion
	public void DragWindow(object sender, MouseButtonEventArgs args)
		=> DragMove();
	public void ExitClicked(object sender, RoutedEventArgs args)
		=> Close();

	public void NiTiSLinkClick(object sender, EventArgs args)
		=> Utils.OpenBrowser("https://github.com/NiTiS-Dev");
	private void EditOrCreateProfile(object sender, RoutedEventArgs e)
	{
		ProfileEditorWindow window = new(profileBox.SelectedItem as ComboBoxItem);
		this.IsEnabled = false;
		window.Show();

		window.Closed += (_, _) =>
		{
			this.IsEnabled = true;
		};
	}
}
