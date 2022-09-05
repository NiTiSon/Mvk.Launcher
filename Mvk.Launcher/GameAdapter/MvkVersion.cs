using System;

namespace Mvk.Launcher.GameAdapter;

public readonly struct MvkVersion
{
	public readonly Version Version;
	public readonly uint VersionID;
	public readonly string DownloadURI;

	public MvkVersion(Version ver, uint id, string downloadURI)
	{
		Version = ver;
		VersionID = id;
		DownloadURI = downloadURI;
	}
}
