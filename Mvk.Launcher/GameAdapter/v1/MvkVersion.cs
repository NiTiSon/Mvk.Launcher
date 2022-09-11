using System;
using System.Runtime.Serialization;

namespace Mvk.Launcher.GameAdapter.v1;

[Serializable]
public readonly struct MvkVersion
{
	public readonly Version Version;
	public readonly string ProviderURI;

	public MvkVersion(Version ver, string downloadURI)
	{
		Version = ver;
		ProviderURI = downloadURI;
	}
}
