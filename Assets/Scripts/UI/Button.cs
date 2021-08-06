using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace UserInterface
{
	public class Button : MonoBehaviour
	{
		public GameObject button;

		public void OnClickButton()
		{
			var mode = ParamBridge.VMode;
			switch (mode)
			{
				case ViewMode.Title:
					switch (button.name)
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
							Debug.Log($"Unknown Button Name: {button.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;

				case ViewMode.StartOption:
					switch (button.name)
					{
						case "HomeButton1":
							ParamBridge.UpdateView(ViewMode.Title);
							break;
						default:
							Debug.Log($"Unknown Button Name: {button.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;

				case ViewMode.Credit:
					switch (button.name)
					{
						case "HomeButton2":
							ParamBridge.UpdateView(ViewMode.Title);
							break;
						default:
							Debug.Log($"Unknown Button Name: {button.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;
				
				case ViewMode.StageList:
					switch (button.name)
					{
						case "Stage1Button":
							ParamBridge.UpdateScreen(ScreenMode.Game);
							break;
						case "Stage2Button":
							ParamBridge.UpdateScreen(ScreenMode.Game);
							break;
						default:
							Debug.Log($"Unknown Button Name: {button.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;
				case ViewMode.InGame:
				switch (button.name)
				{
					case "PauseButton":
					ParamBridge.UpdateView(ViewMode.GameOption);
					break;
				}
				break;

				case ViewMode.GameEnd:
				
				case ViewMode.Pause:
				switch (button.name)
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
				switch (button.name)
				{
				case "RetlyButton":
					ParamBridge.UpdateView(ViewMode.GameBegin);
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
					switch (button.name)
					{
					case "BackButton":
						ParamBridge.UpdateView(ViewMode.Pause);
						break;
					}
					break;
			}
			AudioManager.Instance.PlayClick();
		}

	}
}