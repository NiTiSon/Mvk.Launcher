using System;

namespace Mvk.Launcher.Core;

public class ProgressInfo<TText, TProgress> : IProgress<TProgress>
{
	public TText Text { get; set; }
	public TProgress Progress { get; set; }
	public event Action<TText, TProgress> OnProgress;
	public event Action<bool> OnCompleted;
	public void Report(TProgress value)
	{
		Progress = value;
		OnProgress(Text, Progress);
	}
	public void Report(TText text, TProgress value)
	{
		Text = text;
		Progress = value;
		OnProgress(Text, Progress);
	}
	public void Complete()
		=> OnCompleted(true);
	public void Fail()
		=> OnCompleted(false);

	public ProgressInfo(TText text, TProgress progress)
	{
		Text = text;
		Progress = progress;

		OnProgress = new((_,_) => { });
		OnCompleted = new((_) => { });

	}
}