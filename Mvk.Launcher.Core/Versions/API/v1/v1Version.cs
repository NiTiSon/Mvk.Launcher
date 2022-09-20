using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.Versions.API.v1;

public class v1Version : IVersion
{
	[YamlMember(Alias = "name")]
	public string Name { get; }
	[YamlMember(Alias = "version")]
	public string Version { get; }
	[YamlMember(Alias = "download-uri")]
	public Uri DownloadUri { get; }
	public v1Version(string name, string version, Uri downloadUri)
	{
		Name = name;
		Version = version;
		DownloadUri = downloadUri;
	}
}
