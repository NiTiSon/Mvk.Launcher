using System;
using System.Collections.Generic;

namespace Mvk.Launcher.Core.Versions.API.v0;

public sealed class Mapper : IMapper
{
	private readonly List<v0.Version> versions = new(24);
	public void Clear()
		=> versions.Clear();
	public bool ParseData(string data)
	{
		try
		{
			foreach (string line
			in data.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries))
			{
				if (string.IsNullOrWhiteSpace(line))
					continue;

				if (line.StartsWith("!!!"))
					continue;

				int hash = line.IndexOf('#');
				int semi = line.IndexOf(';');

				string versionName = line.Substring(0, hash);
#if NET50_OR_GREATER
			string uri = semi is -1 ? line[(hash+1)..(line.Length-1)] : line[(hash+1)..semi];
#else
				string uri = semi is -1 ? line.Substring(hash + 1) : line.Substring(hash + 1, (line.Length - 1) - hash - 1);
#endif

				versions.Add(new(versionName, new Uri(uri)));


			}
		}
		catch
		{
			return false;
		}
		return true;
	}
	public IEnumerable<IVersion> GetVersions()
		=> versions;
}
