using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher;

[Serializable]
public sealed class Options
{
	[YamlMember(Alias = "player-name")]
	public string? PlayerName { get; set; } = "Player";
	[YamlMember(Alias = "selected-profile")]
	public string? SelectedVersion { get; set; }
}
