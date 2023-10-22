using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mvk.Launcher.Controls;

public partial class GameInstanceCard : UserControl
{
	public static readonly DependencyProperty ImageProperty;

	public GameInstanceCard()
	{
		InitializeComponent();
	}

	public ImageSource Image
	{
		get { return (ImageSource)GetValue(ImageProperty); }
		set { SetValue(ImageProperty, value); }
	}

	static GameInstanceCard()
	{
		ImageProperty = DependencyProperty.Register(nameof(Image), typeof(ImageSource), typeof(GameInstanceCard));
	}
}
