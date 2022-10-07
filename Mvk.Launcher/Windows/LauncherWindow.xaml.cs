using Mvk.Launcher;
using Mvk.Launcher.Core;
using NiTiS.IO;
using Serilog;
using System;
using System.Windows;
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
		core.Load();
		InitializeComponent();
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
}
