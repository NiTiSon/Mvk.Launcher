using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvk.Launcher.Core.API.v1;

public sealed class Mapper : IMapper<Version>
{
	private readonly List<Version> versions = new(24);
	public void Clear()
		=> versions.Clear();
	public IEnumerable<Version> GetVersions()
		=> versions;
	public bool ParseData(string data)
	{
		foreach (string line in data.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries))
		{
			if (string.IsNullOrWhiteSpace(line))
				continue;

			if (line.FirstOrDefault() == '#')
				continue;

			int split = line.IndexOf('=');

			if (split == -1)
			{
				Log.Error("Invalid syntax");
				continue;
			}

			string vName = line.Substring(0, split).Trim(' ');
			string vVersion = vName;
			string vUri = line.Substring(split + 1).Trim(' ');

			int nameSplit = vName.IndexOf('@');

			if (nameSplit != -1)
			{
				vVersion = vName.Substring(0, nameSplit).Trim(' ');
				vName = vName.Substring(nameSplit + 1).Trim(' ');
			}

			versions.Add(new Version(vName, vVersion, new Uri(vUri)));
		}

		return true;
	}
}
