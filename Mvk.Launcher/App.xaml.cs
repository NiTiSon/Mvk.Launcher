using Mkv.Launcher.Windows;
using Mvk.Launcher.Core;
using NiTiS.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static readonly NiTiSVersion Version = new(1, 1);
	public const string DateFormat = "yy-MM-dd HH:mm:ss zzz";
	public const string LogFormat = $"{{Timestamp:{DateFormat}}} [{{Level:u4}}] {{Message:lj}}{{NewLine}}{{Exception}}";
	public static DateTime StartupTime = DateTime.Now;
	private static readonly Directory MvkFolder = new(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mvk/launcher/"));
	protected override void OnStartup(StartupEventArgs e)
	{
		string logFileName = System.IO.Path.Combine(MvkFolder.Path, $"logs/{StartupTime:yy-MM-dd HH.mm.ss}.log");
		string lastLogFileName = System.IO.Path.Combine(MvkFolder.Path, "logs/last.log");

		new File(lastLogFileName).TryDelete();

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(logFileName, outputTemplate: LogFormat)
			.WriteTo.File(lastLogFileName, outputTemplate: LogFormat)
#if DEBUG
			.WriteTo.Console()
#endif
			.CreateLogger();

		Log.Information("{0}.new()", nameof(Logger));

		base.OnStartup(e);
	}

	private void Launch(object sender, StartupEventArgs e)
	{
		Log.Information("{0}.new()", nameof(App));

		LauncherWindow window = new(MvkFolder);

		window.Show();

		window.Closing += (_, _) =>
		{
			Log.Warning("Saving files before closing...");

			if (window.core is not null)
			{
				window.core.SaveOptionsFile().Wait();

			}

			Log.Information("Window closing...");
			Log.CloseAndFlush();
		};
	}
}
