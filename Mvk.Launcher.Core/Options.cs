using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core;

[Serializable]
public sealed class Options
{
	[YamlMember(Alias = "user-name", Description = "Custom username")]
	public string? UserName { get; set; }
	[YamlMember(Alias = "last-selected", Description = "Last selected profile")]
	public string? SelectedProfile { get; set; }
	[YamlMember(Alias = "action-after-launch", Description = "What to do with the window after the game is launched")]
	public AfterLaunchAction AfterLaunchAction { get; set; } = default;
	public static readonly Options Default = new();
}
