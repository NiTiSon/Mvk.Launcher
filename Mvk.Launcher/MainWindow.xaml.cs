using Mvk.Launcher;
using Mvk.Launcher.GameAdapter;
using Mvk.Launcher.GameAdapter.v0;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers.Rar;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mkv.Launcher;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private static volatile MainWindow This;
	public MainWindow()
	{
		Closed += (o, e) => { LauncherCore.SaveOptions(); };

		LauncherCore.OnVersionsRefreshed += VersionsUpdated;
		LauncherCore.OnOptionsLoaded += OptionsLoaded;

		InitializeComponent();
		This = this;
		LauncherCore.LoadVersions();
		LauncherCore.LoadOptions();
	}
	private bool versionsLoaded = false;
	private void VersionsUpdated()
	{
		versionsLoaded = true;
		this.newGameInstanceButton.IsEnabled = true;
	}
	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);

		this.optionPlayerName.TextChanged -= UpdateName;
	}
	private void OptionsLoaded()
	{
		this.optionPlayerName.Text = LauncherCore.Options.PlayerName;
		this.optionPlayerName.TextChanged += UpdateName;

		this.versionBox.IsEnabled = true;
		RefreshInstancesList();
	}
	private void RefreshInstancesList()
	{
		this.versionBox.Items.Clear();
		foreach (MvkGameInstance instance in LauncherCore.Options.Instances)
		{
			MvkComboBoxItem comboBoxItem = new(instance.Name!, "find:" + instance.Version!);

			this.versionBox.Items.Add(comboBoxItem);

			if (instance.Name == LauncherCore.Options.SelectedInstance)
			{
				this.versionBox.SelectedItem = comboBoxItem;
			}
		}
	}
	private void UpdateName(object sender, TextChangedEventArgs e)
	{
		LauncherCore.Options.PlayerName = String.IsNullOrWhiteSpace(this.optionPlayerName.Text) ? "Player" : this.optionPlayerName.Text;
	}

	public void DragWindow(object sender, MouseButtonEventArgs args)
	{
		DragMove();
	}
	public void ExitClicked(object sender, RoutedEventArgs args)
	{
		this.Close();
	}

	public void VersionNameLoad(object sender, RoutedEventArgs e)
	{
		if (sender is TextBlock tb)
		{
#if DEBUG
			tb.Text = $"DEBUG: {LauncherCore.ApplicationVersion}";
#else
			tb.Text = "Version: " + LauncherCore.ApplicationVersion;
#endif
		}
	}
	public void NiTiSLinkClick(object sender, EventArgs args)
		=> LauncherCore.OpenBrowser("https://github.com/NiTiS-Dev");
	public async void PlayButtonClicked(object sender, RoutedEventArgs e)
	{
		if (this.versionBox.SelectedItem is not MvkComboBoxItem selected)
		{
			LauncherCore.ShowError("Version not selected");

			return;
		}

		string download = selected.DownloadURI;

		if (download.StartsWith("find:"))
		{
			string versionName = download.Substring(5);

			MvkVersion version = LauncherCore.GameVersions.Find((x) => x.Version.ToString(4) == versionName);

			MvkGameInstance instance = LauncherCore.Options.Instances.Find((x) => x.Name == selected.Content as string);

			Button btn = sender as Button;

			btn.IsEnabled = false;
			btn.Content = "Launch...";

			Process gameLaunch = new();

			string exePath = Path.Combine(LauncherCore.VersionsDirectory, version.Version.ToString(4), "MvkLauncher.exe");

			if (!File.Exists(exePath))
			{
				btn.Content = "Downloading...";
				using HttpClient client = new();

				byte[] rarFile = await client.GetByteArrayAsync(version.DownloadURI);

				string tempRar = Path.GetTempFileName();

				File.WriteAllBytes(tempRar, rarFile);

				using RarArchive archive = RarArchive.Open(tempRar);

				LauncherCore.ShowError(archive.IsComplete.ToString());

				string versionDirectory = Path.Combine(LauncherCore.VersionsDirectory, version.Version.ToString(4));

				if (!Directory.Exists(versionDirectory))
					Directory.CreateDirectory(versionDirectory);

				foreach (RarArchiveEntry entry in archive.Entries.Where(e => !e.IsDirectory))
				{
					entry.WriteToDirectory(versionDirectory, new()
					{
						Overwrite = true,
						ExtractFullPath = true,
					});
				}
			}

			if (!File.Exists(Path.Combine(instance.SaveLocation, "options.ini")))
			{
				using StreamWriter sw = File.CreateText(Path.Combine(instance.SaveLocation, "options.ini"));
				sw.WriteLine($"Nickname: {LauncherCore.Options.PlayerName}");
			}

			gameLaunch.EnableRaisingEvents = true;
			gameLaunch.StartInfo.FileName = exePath;
			gameLaunch.StartInfo.WorkingDirectory = instance.SaveLocation;
			gameLaunch.StartInfo.Arguments = "--launcher=nitis-mvk-lnchr";


			
			btn.Content = "Play";

			gameLaunch.Start();
			gameLaunch.Exited += (s, e) =>
			{
				if (LauncherCore.Options.AfterLaunchAction == AfterLaunchAction.Hide)
				{
					Dispatcher.Invoke(delegate { this.Show(); });
				}
			};
			if (LauncherCore.Options.AfterLaunchAction == AfterLaunchAction.Hide)
			{
				this.Hide();
			}
			else if (LauncherCore.Options.AfterLaunchAction == AfterLaunchAction.Exit)
			{
				this.Close();
			}
			btn.IsEnabled = true;
		}
	}
	private void NewInstance(object sender, RoutedEventArgs e)
	{
		if (!versionsLoaded)
			return;

		NewInstanceWindow newInstanceWindow = new();
		newInstanceWindow.Show();
		this.IsEnabled = false;
		newInstanceWindow.Closing += (_, _) =>
		{
			this.IsEnabled = true;

			if (newInstanceWindow is not null)
			{
				MvkGameInstance? instance = newInstanceWindow.createdInstance;

				if (instance is null)
					return;

				if (LauncherCore.Options.Instances.Contains(instance))
				{
					LauncherCore.ShowError("Instance with same name already exists");
					return;
				}	

				LauncherCore.Options.Instances.Add(instance);
				RefreshInstancesList();
			}
		};
	}
	private void GameInstanceSelected(object sender, SelectionChangedEventArgs e)
	{
	if (this.versionBox.SelectedItem is not MvkComboBoxItem item)
			return;

		LauncherCore.Options.SelectedInstance = item.Content as string;
	}
}
