using System;
using YamlDotNet.Serialization;

namespace Mvk.Launcher.Core.API.v1;

[Serializable]
public sealed class VersionsMap
{
	[YamlMember(Alias = "versions")]
	public Entry[] Versions { get; set; }
	public sealed class Entry
	{
		[YamlMember(Alias = "type")]
		public Type Solution { get; set; }
		[YamlMember(Alias = "name")]
		public string Name { get; set; } = string.Empty;
		private string? version;
		[YamlMember(Alias = "version")]
		public string Version
		{
			get => version ?? Name;
			set => version = value;
		}
		[YamlMember(Alias = "uri")]
		public string Uri { get; set; } = string.Empty;
		public enum Type : byte
		{
			v0Mapper,
			v1Version,
			v1Mapper,
		}
	}
}
