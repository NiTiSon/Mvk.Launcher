using System.Collections.Generic;

namespace Mvk.Launcher.Core.API;

public interface IMapper<TVERSION>
{
	public void Clear();
	public bool ParseData(string data);
	public IEnumerable<TVERSION> GetVersions();
}