namespace Mvk.Launcher.Core;


public readonly struct NiTiSVersion
{
	public readonly ushort Version, Revision;
	public NiTiSVersion(ushort version, ushort revision)
	{
		Version = version;
		Revision = revision;
	}
	public override string ToString()
		=> $"V{Version}r{Revision}";
}