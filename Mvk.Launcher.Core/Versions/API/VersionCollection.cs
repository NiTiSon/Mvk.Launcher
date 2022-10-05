using System.Collections.Generic;

namespace Mvk.Launcher.Core.Versions.API;

public sealed class VersionCollection : List<IVersion>
{
	public static readonly short Version = 1;
	public VersionCollection()
	{

	}
}
