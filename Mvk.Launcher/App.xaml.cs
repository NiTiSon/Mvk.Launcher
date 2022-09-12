using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File("logs/last.log")
			.WriteTo.File($"logs/{DateTime.Now:HH:mm.ss-yyyy/MM/dd}.log")
			.CreateLogger();
	}
}
