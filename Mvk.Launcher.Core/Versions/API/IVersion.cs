using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvk.Launcher.Core.Versions.API;

public interface IVersion
{
	public string Name { get; }
	public string Version { get; }
	public Uri DownloadUri { get; }
}
