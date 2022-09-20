using Mkv.Launcher.Windows;
using Mvk.Launcher.Core;
using NiTiS.IO;
using Serilog;
using System;
using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private static readonly Directory MvkFolder = new(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/"));
	private static readonly Directory VersionFolder = MvkFolder.SubDirectory("versions");
	public static MvkLauncher Launcher { get; } = new(MvkFolder, VersionFolder);
	protected override void OnStartup(StartupEventArgs e)
	{
		new File(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/", "logs/last.log")).TryDelete();
		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/", "logs/last.log"))
			.WriteTo.File(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/", $"logs/{DateTime.Now:HH:mm.ss-yyyy/MM/dd}.log"))
			.CreateLogger();

		base.OnStartup(e);
	}

	private new void Launch(object sender, StartupEventArgs e)
	{
		LauncherWindow window = new();

		window.Show();

		window.Closing += (_, _) =>
		{
		};
	}
}
