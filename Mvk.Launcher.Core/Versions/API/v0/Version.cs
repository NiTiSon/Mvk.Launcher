using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.Versions.API.v0;

public sealed class Version : IVersion
{
	[YamlMember(Alias = "name")]
	public string Name => VersionString;

	[YamlMember(Alias = "version")]
	public string VersionString { get; }

	[YamlMember(Alias = "download-uri")]
	public Uri DownloadUri { get; }
	public Version(string version, Uri downloadUri)
	{
		VersionString = version;
		DownloadUri = downloadUri;
	}
}
