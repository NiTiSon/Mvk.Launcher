using Mvk.Launcher.Core;
using System;
using System.Diagnostics;
using System.Windows;
using YamlDotNet.Serialization;

namespace Mvk.Launcher;

public static class Utils
{
	[Obsolete("Replace by custom error handler")]
	public static MessageBoxResult ShowError(this LauncherCore launcher, Exception ex)
	{
		string log = LauncherCore.ShowError(ex);
		return MessageBox.Show(log, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}
	public static void OpenBrowser(Uri link)
		=> OpenBrowser(link.AbsoluteUri);
	public static void OpenBrowser(string link)
	{
		using Process process = new();
		process.StartInfo.FileName = "cmd.exe";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.Arguments = $"/C start {link}";

		process.Start();
	}
	private static readonly IDeserializer deserializer = new DeserializerBuilder()
		.Build();
	private static readonly ISerializer serializer = new SerializerBuilder()
		.Build();
}