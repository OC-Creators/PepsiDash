using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using UnityEngine.FadeManager;

namespace UserInterface{
	
	public class Scenemove : MonoBehaviour {
		public GameObject btn;
		public Text clearText;
		public GameObject Active;
		public GameObject NotActive;

		public void OnClickStartButton()
		{
			if(btn.name=="StartButton")FadeManager.Instance.LoadScene("StageSelectScreen",1.0f);
			if(btn.name=="Stage1Button")FadeManager.Instance.LoadScene("GameScreen_NETA",1.0f);
			if(btn.name=="TwitterButton"){
				var url = "https://twitter.com/intent/tweet?"
					+ "text=" + "今回の記録は『"
					+ clearText.text
					+ "』点";
				Application.OpenURL(url);
			}
			if(btn.name=="OptionButton" || btn.name=="CreditButton"){
				Active.SetActive(true);
				NotActive.SetActive(false);
			}

		}
	}
}