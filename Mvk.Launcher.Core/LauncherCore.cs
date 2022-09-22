using Mvk.Launcher.Core.Versions.API;
using NiTiS.NBT;
using NiTiS.IO;
using Serilog;
using System;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core;

public sealed class LauncherCore
{
	private readonly File optionsFile, versionsFile;
	private readonly Directory saveDir;
	private static readonly ISerializer serializer;
	private static readonly IDeserializer deserializer;
	public Options Options = Options.Default;
	public VersionCollection Versions;
	public LauncherCore(Directory saveDirectory)
	{
		saveDir = saveDirectory;
		optionsFile = saveDir.File("options.yml");
		versionsFile = saveDir.File("versions.nbt");
	}
	public void Load()
	{
		Task opt = LoadOptionFile();
		Task ver = LoadVersionsFile();

		Task.WaitAll(opt, ver);
	}
	public void Save()
	{

	}
	public async Task LoadVersionsFile()
	{
		if (versionsFile.Exists)
		{
			try
			{
				using FStream versionsStream = versionsFile.Read();

				using NBTReadStream reader = new(versionsStream);

				Tag parentTag = reader.ReadTag();

				if (parentTag is CompoundTag cTag)
				{
					short version = (cTag["fileVersion"] as ShortTag).Int16;

					if (version == VersionCollection.Version)
					{
						Versions = new(cTag);
					}
					else
					{
						Log.Warning("{0:Name} is outdated", versionsFile);
						Versions = new();
					}
				}
				return;
			}
			catch (Exception exception)
			{
				Log.Fatal("Unable to read {0:Name}", versionsFile);
			}
		}
		Versions = new();
	}
	public async Task LoadOptionFile()
	{
		if (optionsFile.Exists)
		{
			try
			{
				using FStream optionsStream = optionsFile.Read();

				using System.IO.StreamReader reader = new(optionsStream);

				Options = deserializer.Deserialize<Options>(await reader.ReadToEndAsync());
				return;
			}
			catch (Exception exception)
			{
				Log.Fatal("Unable to read {0:Name}", optionsFile);
			}
		}
		Options = Options.Default;
	}
	static LauncherCore()
	{
		serializer = new SerializerBuilder()
			.Build();
		deserializer = new DeserializerBuilder()
			.Build();
	}
}
