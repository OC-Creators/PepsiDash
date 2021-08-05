using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace UserInterface
{
	public class Button : MonoBehaviour
	{
		public AudioManager audioManager;
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
							ParamBridge.UpdateView(ViewMode.Option);
							break;
						case "CreditButton":
							ParamBridge.UpdateView(ViewMode.Credit);
							break;
						default:
							Debug.Log($"Unknown Button Name: {button.name} in {mode.ToStringQuickly()}");
							break;
					}
					break;

				case ViewMode.Option:
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
			}
		}
		public void PlayClick()
		{
			audioManager.PlayClick();
		}
	}
}