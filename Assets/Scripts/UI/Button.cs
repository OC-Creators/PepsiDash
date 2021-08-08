using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace UserInterface
{
	public class Button : MonoBehaviour
	{
		public void OnClickButton(AudioClip clip)
		{
			var mode = ParamBridge.VMode;
			switch (mode)
			{
				case ViewMode.Title:
					switch (gameObject.name)
					{
						case "StartButton":
							ParamBridge.UpdateScreen(ScreenMode.StageSelect);
							break;
						case "OptionButton":
							ParamBridge.UpdateView(ViewMode.StartOption);
							break;
						case "CreditButton":
							ParamBridge.UpdateView(ViewMode.Credit);
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
							ParamBridge.UpdateView(ViewMode.Title);
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
							ParamBridge.UpdateView(ViewMode.Title);
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
							ParamBridge.UpdateScreen(ScreenMode.Game);
							break;
						case "Stage2Button":
							ParamBridge.UpdateScreen(ScreenMode.Game);
							break;
						default:
							Debug.Log($"Unknown Button Name: {gameObject.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;
				case ViewMode.InGame:
					switch (gameObject.name)
					{
						case "PauseButton":
						ParamBridge.UpdateView(ViewMode.Pause);
						break;
					}
					break;

				case ViewMode.GameEnd:
					break;
				
				case ViewMode.Pause:
					switch (gameObject.name)
					{
						case "ResumeButton":
							ParamBridge.UpdateView(ViewMode.InGame);
							break;

						case "OptionButton":
							ParamBridge.UpdateView(ViewMode.GameOption);
							break;
					}
					break;

				case ViewMode.Result:
					switch (gameObject.name)
					{
						case "RetryButton":
							ParamBridge.UpdateScreen(ScreenMode.Game);
							break;

						case "TitleButton":
							ParamBridge.UpdateScreen(ScreenMode.Start);
							break;

						case "TwitterButton":
							var url = $"https://twitter.com/intent/tweet?text=今回の記録は『{35}』点";
							Application.OpenURL(url);
							break;
					}
					break;

				case ViewMode.GameOption:
					switch (gameObject.name)
					{
						case "BackButton":
							ParamBridge.UpdateView(ViewMode.Pause);
							break;
					}
					break;
			}
			AudioManager.Instance.PlayClick(clip);
		}

	}
}