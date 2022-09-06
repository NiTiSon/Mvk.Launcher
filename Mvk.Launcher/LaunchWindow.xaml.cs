﻿using System.Windows;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for LaunchWindow.xaml
/// </summary>
public partial class LaunchWindow : Window
{
	public LaunchWindow()
	{
		InitializeComponent();
	}

	public void ExitClicked(object sender, RoutedEventArgs args)
	{
		this.Close();
	}
}
