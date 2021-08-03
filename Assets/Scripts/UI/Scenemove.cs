using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
//using UnityEngine.FadeManager;

public class Scenemove : MonoBehaviour {
	public GameObject btn;

	public void OnClickStartButton()
	{
		if(btn.name=="Button")FadeManager.Instance.LoadScene("StageSelectScreen",1.0f);
		if(btn.name=="Stage1Button")FadeManager.Instance.LoadScene("GameScreen_NETA",1.0f);
		//Debug.Log(btn.name);
	}

}
