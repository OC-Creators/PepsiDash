using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

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
						General.ParamBridge.UpdateScreen(ScreenMode.StageSelect);
						break;
					case "OptionButton":
						General.ParamBridge.UpdateView(ViewMode.Option);
						break;
					case "CreditButton":
						General.ParamBridge.UpdateView(ViewMode.Credit);
						break;
				}
				break;
			case ViewMode.StageList:
				switch(button.name) {
					case "Stage1Button":
						General.ParamBridge.UpdateScreen(ScreenMode.Game);
						break;
					case "Stage2Button":
						General.ParamBridge.UpdateScreen(ScreenMode.Game);
						break;
				}
				break;
		}
	}
}
}