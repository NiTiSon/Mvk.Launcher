using System.Collections.Generic;

namespace Mvk.Launcher.Core;

public sealed class ProfileCollection : List<Profile>
{
	public ProfileCollection() { }
	public ProfileCollection(int capacity) : base(capacity) { }
	public ProfileCollection(IEnumerable<Profile> collection) : base(collection) { }
}