using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
//using UnityEngine.FadeManager;

public class Home_start : MonoBehaviour {

	public void OnClickStartButton()
	{
		FadeManager.Instance.LoadScene("StageSelectScreen",1.0f);
		//SceneManager.LoadScene("StageSelectScreen");
	}

}
