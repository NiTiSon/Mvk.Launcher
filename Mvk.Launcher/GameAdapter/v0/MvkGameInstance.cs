using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.GameAdapter;

[Serializable]
public sealed class MvkGameInstance
{
	[YamlMember(Alias = "name")]
	public string? Name { get; set; }
	[YamlMember(Alias = "favorite")]
	public bool Favorite { get; set; }
	[YamlMember(Alias = "game-version")]
	public string? Version { get; set; }
	[YamlMember(Alias = "save-path")]
	public string? SaveLocation { get; set; }
}