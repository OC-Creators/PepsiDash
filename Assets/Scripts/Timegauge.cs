using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timegauge : MonoBehaviour
{

	public Image image;
	private float timeleft;
	public GameObject Clear;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timeleft -= Time.deltaTime;
		if (timeleft <= 0.0) {
			timeleft = 1.0f;
			image.fillAmount -= 0.1f;
			//ここに処理
		}
		else if(image.fillAmount==0){
			//Debug.Log("あ");
			Clear.SendMessage("OnEnter");
	}
}
}
