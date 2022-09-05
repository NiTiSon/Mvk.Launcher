using System.Net.Http;

namespace Mvk.Launcher.GameAdapter;

public class VersionChecker
{
	private readonly HttpClient client;
	private readonly string requestURL;
	public VersionChecker(string versionstxtURL)
	{
		client = new();
		requestURL = versionstxtURL;
	}
}
