using System;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.API.v1;

[Serializable]
public class Version
{
	[YamlMember(Alias = "name")]
	public string Name { get; set; }
	[YamlMember(Alias = "version")]
	public string VersionString { get; set; }
	[YamlMember(Alias = "download-uri", SerializeAs = typeof(string))]
	public Uri DownloadUri { get; set; }
	public Version() { }
	public Version(string name, string version, Uri downloadUri)
	{
		Name = name;
		VersionString = version;
		DownloadUri = downloadUri;
	}
	public override string ToString()
		=> $"{Name} {(Name != VersionString ? "v:" + VersionString : string.Empty)}";
}
