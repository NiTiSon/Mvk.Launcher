using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mvk.Launcher;

public class MvkComboBoxItem : ComboBoxItem
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
}
