using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timegauge : MonoBehaviour
{

	public Image image;
	private float timeleft;
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
			image.fillAmount -= 0.01f;
			//ここに処理
		}

	}
}
