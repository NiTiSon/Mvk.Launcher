﻿using Mvk.Launcher.GameAdapter;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Mvk.Launcher;

[Serializable]
public sealed class Options
{
	[YamlMember(Alias = "player-name")]
	public string? PlayerName { get; set; } = "Player";
	[YamlMember(Alias = "selected-instance")]
	public string? SelectedInstance { get; set; }
	[YamlMember(Alias = "action-after-launch")]
	public AfterLaunchAction AfterLaunchAction { get; set; } = AfterLaunchAction.Exit;
	[YamlMember(Alias = "instances")]
	public List<MvkGameInstance> Instances { get; set; } = new(0);
}

public enum AfterLaunchAction : byte
{
	None = 0,
	Exit = 1,
	Hide = 2,
}