using System;
using System.Diagnostics;

namespace Mvk.Launcher;

public static class Utils
{
	public static void OpenBrowser(Uri link)
		=> OpenBrowser(link.AbsoluteUri);
	public static void OpenBrowser(string link)
	{
		using Process process = new();

		if (Environment.OSVersion.Platform == PlatformID.Unix)
		{
			process.StartInfo.FileName = "xdg-open";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.Arguments = $"{link}";
		}
		else
		{
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.Arguments = $"/C start {link}";
		}

		process.Start();
	}
}