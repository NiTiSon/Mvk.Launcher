using NiTiS.NBT;
using System.Collections.Generic;

namespace Mvk.Launcher.Core.Versions.API;

public sealed class VersionCollection : List<IVersion>
{
	public static readonly short Version = 1;
	public VersionCollection()
	{

	}
	public VersionCollection(CompoundTag tag)
	{

	}

	public CompoundTag Save()
	{
		CompoundTag tag = new("root", new Dictionary<string, Tag>());

		tag["version"] = new ShortTag("version", Version);



		return tag;
	}
}
