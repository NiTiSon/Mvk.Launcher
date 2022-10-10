using Mvk.Launcher.Core.API.v0;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static Mvk.Launcher.Core.API.v1.VersionsMap.Entry.Type;

namespace Mvk.Launcher.Core.API;

public sealed class VersionCollection : List<v1.Version>
{
	public VersionCollection() { }
	public VersionCollection(int capacity) : base(capacity) { }
	public VersionCollection(IEnumerable<v1.Version> collection) : base(collection) { }
	public async Task Resolve(v1.VersionsMap.Entry entry, HttpClient net)
	{
		switch (entry.Solution)
		{
			case v1Version:

				Add(new v1.Version(entry.Name, entry.Version, new Uri(entry.Uri)));

				break;
			case v0Mapper:
				try
				{
					v0.Mapper mapper = new();
					string v0List = net.GetStringAsync(entry.Uri).Result;

					if (mapper.ParseData(v0List))
					{
						AddRange(mapper.GetVersions());
					}
					else
						Log.Error("Unable to parse {0}", entry.Uri);

				}
				catch (Exception exception)
				{
					Log.Fatal("{0}: {1}", exception.GetType().Name, exception.Message);
				}
				
				break;
			case v1Mapper:
				try
				{
					v1.Mapper mapper = new();

					if (mapper.ParseData(net.GetStringAsync(entry.Uri).Result))
					{
						AddRange(mapper.GetVersions());
					}
					else
						Log.Error("Unable to parse {0}", entry.Uri);

				}
				catch (Exception exception)
				{
					Log.Fatal("{0}: {1}", exception.GetType().Name, exception.Message);
				}

				break;
		}
	}
}
