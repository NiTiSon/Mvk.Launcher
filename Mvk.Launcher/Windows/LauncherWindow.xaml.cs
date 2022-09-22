using Mvk.Launcher;
using Mvk.Launcher.Core;
using Mvk.Launcher.Core.Versions.API;
using NiTiS.IO;
using System;
using System.Windows;
using System.Windows.Input;

namespace Mkv.Launcher.Windows;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LauncherWindow : Window
{
	private LauncherCore core;
	public LauncherWindow(Directory saveDirectory)
	{
		core = new(saveDirectory);
		core.Load();
		InitializeComponent();
	}
	public void DragWindow(object sender, MouseButtonEventArgs args)
		=> DragMove();
	public void ExitClicked(object sender, RoutedEventArgs args)
		=> Close();

	public void NiTiSLinkClick(object sender, EventArgs args)
		=> Utils.OpenBrowser("https://github.com/NiTiS-Dev");
}
