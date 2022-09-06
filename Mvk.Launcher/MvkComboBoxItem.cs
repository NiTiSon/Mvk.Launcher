using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mvk.Launcher;

public class MvkComboBoxItem : ComboBoxItem, IEquatable<MvkComboBoxItem>
{
	public string DownloadURI { get; set; }
	public MvkComboBoxItem()
	{
	}
	public MvkComboBoxItem(string versionName, string downloadURI)
	{
		this.Content = versionName;
		this.DownloadURI = downloadURI;
	}

	public bool Equals(MvkComboBoxItem other)
	{
		if (ReferenceEquals(this, other)) return true;

		if (other is null)
			return false;

		if (this.DownloadURI != "ignore" || other?.DownloadURI != "ignore")
		{
			if (this.DownloadURI != other?.DownloadURI)
				return false;
		}

		return this.Content as string == other.Content as string;
	}

	public static bool operator ==(MvkComboBoxItem left, MvkComboBoxItem right)
		=> left.Equals(right);
	public static bool operator !=(MvkComboBoxItem left, MvkComboBoxItem right)
		=> !left.Equals(right);
}
