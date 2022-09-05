using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mvk.Launcher;

public static class LauncherCore
{
	public static Version ApplicationVersion = new(1, 0);

	public static MessageBoxResult ShowError(string errorText)
		=> MessageBox.Show(errorText, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
}