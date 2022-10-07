using System.Collections.Generic;

namespace Mvk.Launcher.Core.API;

public interface IMapper
{
	public void Clear();
	public bool ParseData(string data);
	public IEnumerable<IVersion> GetVersions();
}