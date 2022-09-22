using Mkv.Launcher.Windows;
using Mvk.Launcher.Core;
using NiTiS.IO;
using Serilog;
using Serilog.Events;
using System;
using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public const string DateFormat = "yy-MM-dd HH:mm:ss zzz";
	public const string LogFormat = $"{{Timestamp:{DateFormat}}} [{{Level:u4}}] {{Message:lj}}{{NewLine}}{{Exception}}";
	public static DateTime StartupTime = DateTime.Now;
	private static readonly Directory MvkFolder = new(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/"));
	protected override void OnStartup(StartupEventArgs e)
	{
		string logFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/", $"logs/{StartupTime:yy-MM-dd HH.mm.ss}.log");
		string lastLogFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/", "logs/last.log");

		new File(lastLogFileName).TryDelete();

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(logFileName, outputTemplate: LogFormat)
			.WriteTo.File(lastLogFileName, outputTemplate: LogFormat)
			.CreateLogger();

		Log.Information("Logger initialized");

		base.OnStartup(e);
	}

	private void Launch(object sender, StartupEventArgs e)
	{
		Log.Information("Launch app");

		LauncherWindow window = new(MvkFolder);

		window.Show();

		window.Closing += (_, _) =>
		{
			Log.Information("Window closing...");
			Log.CloseAndFlush();
		};
	}
}
