using Mvk.Launcher.Core;
using Mvk.Launcher.GameAdapter.v0;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YamlDotNet.Serialization;

namespace Mvk.Launcher;

public static class Utils
{
	public static MessageBoxResult ShowError(this MvkLauncher launcher, Exception ex)
	{
		string log = launcher.ShowError(ex);
		return MessageBox.Show(log, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}
	public static void OpenBrowser(string link)
	{
		using Process process = new();
		process.StartInfo.FileName = "explorer.exe";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.Arguments = link;

		process.Start();
	}
	private static readonly IDeserializer deserializer = new DeserializerBuilder()
		.Build();
	private static readonly ISerializer serializer = new SerializerBuilder()
		.Build();
	public static void LoadOptions()
	{
		if (!Directory.Exists(HomeDirectory))
			Directory.CreateDirectory(HomeDirectory);

		string optionsPath = Path.Combine(HomeDirectory, "options.yml");

		if (!File.Exists(optionsPath))
			File.Create(optionsPath).Close();

		try
		{
			Options.Options options = deserializer.Deserialize<Options.Options>(File.ReadAllText(optionsPath));

			if (options is not null)
				Options = options;
			//TODO: Realize instances
		}
		catch (Exception ex)
		{
			ShowError(ex);
		}
		OnOptionsLoaded();
	}
	public static void SaveOptions()
	{
		string optionsPath = Path.Combine(HomeDirectory, "options.yml");

		string yml = serializer.Serialize(Options);

		File.WriteAllText(optionsPath, yml);
	}
	public static async Task LoadVersions()
	{
		HttpClient client = new();

		string versions = await client.GetStringAsync(VersionsURI);

		GameVersions.Clear();
		uint i = 0;
		foreach (string line in versions.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
		{
			if (line.StartsWith("!"))
				continue;

			int hash = line.IndexOf('#');
			int semi = line.IndexOf(';');

			string versionName = line.Substring(0, hash);
#if NET50_OR_GREATER
			string uri = semi is -1 ? line[(hash+1)..(line.Length-1)] : line[(hash+1)..semi];
#else
			string uri = semi is -1 ? line.Substring(hash + 1) : line.Substring(hash + 1, (line.Length - 1) - hash - 1);
#endif

			GameVersions.Add(new(Version.Parse(versionName), i++, uri));
		}

		OnVersionsRefreshed();
	}
}