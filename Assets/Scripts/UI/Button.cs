using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace UserInterface
{
	public class Button : MonoBehaviour
	{
		private ParamBridge pb;
		void Start()
		{
			pb = ParamBridge.Instance;
		}
		public void OnClickButton(AudioClip clip)
		{
			var mode = pb.VMode;
			switch (mode)
			{
				case ViewMode.Title:
					switch (gameObject.name)
					{
						case "StartButton":
							pb.UpdateScreen(ScreenMode.StageSelect);
							break;
						case "OptionButton":
							pb.UpdateView(ViewMode.StartOption);
							break;
						case "CreditButton":
							pb.UpdateView(ViewMode.Credit);
							break;
						default:
							Debug.Log($"Unknown Button Name: {gameObject.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;

				case ViewMode.StartOption:
					switch (gameObject.name)
					{
						case "HomeButton1":
							pb.UpdateView(ViewMode.Title);
							pb.BGMVolume = AudioManager.Instance.Source.volume;
							pb.SEVolume = AudioManager.Instance.SEVolume;
							break;
						default:
							Debug.Log($"Unknown Button Name: {gameObject.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;

				case ViewMode.Credit:
					switch (gameObject.name)
					{
						case "HomeButton2":
							pb.UpdateView(ViewMode.Title);
							break;
						default:
							Debug.Log($"Unknown Button Name: {gameObject.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;
				
				case ViewMode.StageList:
					switch (gameObject.name)
					{
						case "Stage1Button":
							pb.UpdateScreen(ScreenMode.Game);
							break;
						case "Stage2Button":
							pb.UpdateScreen(ScreenMode.Game);
							break;
						/*
						case "RightButton":

							break;
						case "LeftButton":

							break;
							*/
						default:
							Debug.Log($"Unknown Button Name: {gameObject.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;
				case ViewMode.InGame:
					switch (gameObject.name)
					{
						case "PauseButton":
						pb.UpdateView(ViewMode.Pause);
						break;
					}
					break;

				case ViewMode.GameEnd:
					break;
				
				case ViewMode.Pause:
					switch (gameObject.name)
					{
						case "ResumeButton":
							pb.UpdateView(ViewMode.InGame);
							break;

						case "OptionButton":
							pb.UpdateView(ViewMode.GameOption);
							break;
					}
					break;

				case ViewMode.Result:
					switch (gameObject.name)
					{
						case "RetryButton":
							pb.UpdateScreen(ScreenMode.Game);
							break;

						case "TitleButton":
							pb.UpdateScreen(ScreenMode.Start);
							break;

						case "TwitterButton":
							var url = $"https://twitter.com/intent/tweet?text=今回の記録は『{pb.HighScore}』点";
							Application.OpenURL(url);
							break;
					}
					break;

				case ViewMode.GameOption:
					switch (gameObject.name)
					{
						case "BackButton":
							pb.UpdateView(ViewMode.Pause);
							pb.BGMVolume = AudioManager.Instance.Source.volume;
							pb.SEVolume = AudioManager.Instance.SEVolume;
							break;
					}
					break;
			}
			AudioManager.Instance.PlayClick(clip);
		}

	}
}