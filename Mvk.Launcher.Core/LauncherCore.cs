using Mvk.Launcher.Core.API;
using NiTiS.IO;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core;

public sealed class LauncherCore
{
	private readonly File optionsFile, profilesFile, versionsFile;
	private readonly Directory saveDir;
	private readonly HttpClient net;
	private static readonly ISerializer serializer;
	private static readonly IDeserializer deserializer;
	public Options Options = Options.Default;
	public ProfileCollection Profiles = new(1);
	public VersionCollection Versions = new(1);
	public LauncherCore(Directory saveDirectory)
	{
		Log.Information("{0}.new()", nameof(LauncherCore));
		saveDir = saveDirectory;
		optionsFile = saveDir.File("options.yml");
		profilesFile = saveDir.File("profiles.yml");
		versionsFile = saveDir.File("versions.yml");
		net = new HttpClient();
	}
	public void Load()
	{
		Log.Information("{0}.Load()", nameof(LauncherCore));
		Task opt = LoadOptionsFile();
		Task ver = LoadVersions();

		Task.WaitAll(opt, ver);
		Log.Information("{0} Loading completed", nameof(LauncherCore));
	}
	public void Save()
	{
		Task opt = SaveOptionsFile();

		Task.WaitAll(opt);
		Log.Information("{0} Saving completed", nameof(LauncherCore));
	}
	public async Task LoadOptionsFile()
	{
		Log.Information("{0}.LoadOptionsFile()", nameof(LauncherCore));

		Options = Options.Default;
		if (optionsFile.Exists)
		{
			try
			{
				using FStream optionsStream = optionsFile.Read();
				using System.IO.BinaryReader reader = new(optionsStream);

				Options = deserializer.Deserialize<Options>(Encoding.UTF8.GetString(reader.ReadBytes((int)optionsStream.Length)));
			}
			catch (Exception exception)
			{
				Log.Fatal("{0}: {1}", exception.GetType().Name, exception.Message);
			}
		}
		else
			await SaveOptionsFile();
	}
	private async Task SaveOptionsFile()
	{
		Log.Information("{0}.SaveOptionsFile()", nameof(LauncherCore));
		try
		{
			using FStream stream = optionsFile.Open(FileMode.OpenOrCreate);
			using System.IO.StreamWriter writer = new(stream);

			await writer.WriteAsync(serializer.Serialize(Options));
		}
		catch (Exception exception)
		{
			Log.Fatal("{0}: {1}", exception.GetType().Name, exception.Message);
		}
	}
	public async Task LoadVersions()
	{
		Log.Information("{0}.LoadVersions()", nameof(LauncherCore));
		try
		{
			HttpResponseMessage response = net.GetAsync("https://raw.githubusercontent.com/NiTiS-Dev/Mvk.Launcher.Repos/app/api/v1/versions.yml").Result;

			if (!response.IsSuccessStatusCode)
				throw new HttpRequestException("Error code " + response.StatusCode);

			API.v1.VersionsMap vers = deserializer.Deserialize<API.v1.VersionsMap>(await response.Content.ReadAsStringAsync());

			foreach (API.v1.VersionsMap.Entry entry in vers.Versions)
			{
				await Versions.Resolve(entry, net);
			}
		}
		catch (Exception exception)
		{
			Log.Fatal("{0}: {1}", exception.GetType().Name, exception.Message);
		}
	}
	static LauncherCore()
	{
		serializer = new SerializerBuilder()
			.Build();
		deserializer = new DeserializerBuilder()
			.Build();
	}
}
