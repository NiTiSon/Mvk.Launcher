using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.Versions.API.v1;

public class Version : IVersion
{
	[YamlMember(Alias = "name")]
	public string Name { get; }
	[YamlMember(Alias = "version")]
	public string VersionString { get; }
	[YamlMember(Alias = "download-uri")]
	public Uri DownloadUri { get; }
	public Version(string name, string version, Uri downloadUri)
	{
		Name = name;
		VersionString = version;
		DownloadUri = downloadUri;
	}
}
