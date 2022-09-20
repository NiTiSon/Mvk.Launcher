using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.Versions.API.v0;

public sealed class v0Version : IVersion
{
	[YamlMember(Alias = "name")]
	public string Name => Version;

	[YamlMember(Alias = "version")]
	public string Version { get; }

	[YamlMember(Alias = "download-uri")]
	public Uri DownloadUri { get; }
	public v0Version(string version, Uri downloadUri)
	{
		Version = version;
		DownloadUri = downloadUri;
	}
}
