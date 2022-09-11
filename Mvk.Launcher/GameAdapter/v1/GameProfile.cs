using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.GameAdapter.v1;

public class GameProfile
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
