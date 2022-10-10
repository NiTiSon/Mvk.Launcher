using NiTiS.IO;
using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core;

[Serializable]
public sealed class Profile
{
	[YamlMember(Alias = "name")]
	public string Name { get; set; }
	[YamlMember(Alias = "version")]
	public string VersionName { get; set; }
	[YamlMember(Alias = "save-directory", SerializeAs = typeof(string))]
	public Directory? SaveDirectory { get; set; }
}