using NiTiS.IO;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core;

public sealed class MvkLauncher
{
	private readonly File saveFile;
	private readonly Directory saveDir, versionsDir;
	private static readonly ISerializer serializer;
	private static readonly IDeserializer deserializer;
	public Options Options = Options.Default;
	public MvkLauncher(Directory saveDirectory, Directory versionDirectory)
	{
		saveDir = saveDirectory;
		versionsDir = versionDirectory;
		saveFile = saveDir.File("mvk-launcher.yml");
	}
	/// <summary>
	/// Load save file
	/// </summary>
	public async Task Initialize()
	{
		Log.Information("Initialize launcher...");
		if (saveFile.Exists)
		{
			try
			{
				using FStream optionsStream = saveFile.Read();

				using System.IO.StreamReader reader = new(optionsStream);
				
				Options = deserializer.Deserialize<Options>(await reader.ReadToEndAsync());
				return;
			}
			catch (Exception exception)
			{
				Log.Fatal("Unable to read options file");
				ShowError(exception);
			}
		}
		Options = Options.Default;
	}
	public static string ShowError(Exception error)
	{
		string log = $"Exception: {error.GetType().FullName}\nSource: {error.Source}\nStackTrace: {error.StackTrace}\nMessage: {error.Message}";
		Log.Error(log);
		return log;
	}
	static MvkLauncher()
	{
		serializer = new SerializerBuilder()
			.Build();
		deserializer = new DeserializerBuilder()
			.Build();
	}
}
