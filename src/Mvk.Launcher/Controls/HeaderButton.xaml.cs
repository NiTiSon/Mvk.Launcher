using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mvk.Launcher.Controls;

public partial class HeaderButton : Button
{
	public static readonly DependencyProperty ImageSourceProperty;

	public HeaderButton()
	{
		InitializeComponent();
	}

	public ImageSource ImageSource
	{
		get { return (ImageSource)GetValue(ImageSourceProperty); }
		set { SetValue(ImageSourceProperty, value); }
	}

	static HeaderButton()
	{
		ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(HeaderButton), new PropertyMetadata(null!));
		
		DefaultStyleKeyProperty.OverrideMetadata(
			typeof(HeaderButton),
			new FrameworkPropertyMetadata(typeof(HeaderButton)));
	}
}
