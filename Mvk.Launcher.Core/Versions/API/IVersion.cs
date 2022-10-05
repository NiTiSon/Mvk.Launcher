using System;

namespace Mvk.Launcher.Core.Versions.API;

public interface IVersion
{
	public string Name { get; }
	public string VersionString { get; }
	public Uri DownloadUri { get; }
}
