using Mvk.Launcher.GameAdapter.v1;
using System.Windows;

namespace Mvk.Launcher.Windows;
/// <summary>
/// Interaction logic for GameProfileEditor.xaml
/// </summary>
public partial class GameProfileEditor : Window
{
	public GameProfile profile;
	public GameProfileEditor(GameProfile? profile)
	{
		this.profile = profile;
		InitializeComponent();
	}
}
