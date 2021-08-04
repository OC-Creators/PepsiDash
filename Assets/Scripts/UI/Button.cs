using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface{
	
	public class Button : MonoBehaviour
	{
		public ViewMode mode;
		public Button button;
		public void OnClickButton()
		{
			switch(mode){
			case ViewMode.Title:
				switch(button.name) {
				case "StartButton":
					General.StartScreenManager.UpdateScreen(ScreenMode.StageSelect);
					break;
				case "OptionButton":
					General.StartScreenManager.UpdateView(ViewMode.Option);
					break;
				case "CreditButton":
					General.StartScreenManager.UpdateView(ViewMode.Credit);
					break;
				}
			case ViewMode.StageList:
				switch(button.name) {
				case "Stage1Button":
					General.StageSelectScreenManager.UpdateScreen(ScreenMode.Game);
					break;
				case "Stage2Button":
					General.StageSelectScreenManager.UpdateScreen(ScreenMode.Game);
					break;
				}
		}
	}
}
}