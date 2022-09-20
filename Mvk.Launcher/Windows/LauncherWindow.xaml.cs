using Mvk.Launcher;
using Mvk.Launcher.Core;
using Mvk.Launcher.Core.Versions.API;
using System;
using System.Windows;
using System.Windows.Input;

namespace Mkv.Launcher.Windows;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LauncherWindow : Window
{
	public LauncherWindow()
	{
		App.Launcher.Initialize();
		InitializeComponent();
	}
	public void DragWindow(object sender, MouseButtonEventArgs args)
		=> DragMove();
	public void ExitClicked(object sender, RoutedEventArgs args)
		=> Close();

	public void NiTiSLinkClick(object sender, EventArgs args)
		=> Utils.OpenBrowser("https://github.com/NiTiS-Dev");
}
