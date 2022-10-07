using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mvk.Launcher.App.Windows;
/// <summary>
/// Interaction logic for ProfileEditorWindow.xaml
/// </summary>
public partial class ProfileEditorWindow : Window
{
	public ProfileEditorWindow()
	{
		InitializeComponent();
	}
	public void DragWindow(object sender, MouseButtonEventArgs args)
		=> DragMove();
	public void ExitClicked(object sender, RoutedEventArgs args)
		=> Close();
}
