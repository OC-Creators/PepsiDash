using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
//using UnityEngine.FadeManager;

public class Select_home : MonoBehaviour {

	public void OnClickStartButton()
	{
		FadeManager.Instance.LoadScene("StartScreen",1.0f);
		//SceneManager.LoadScene("StageSelectScreen");
	}

}
