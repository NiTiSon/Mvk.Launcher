using System;

namespace Mvk.Launcher.Core.Versions.API;

public interface IVersion
{
	public string Name { get; }
	public string Version { get; }
	public Uri DownloadUri { get; }
}
