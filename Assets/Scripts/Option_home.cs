using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_home : MonoBehaviour
{
	public GameObject Option;
	public GameObject Home;
	public Slider slider;
	public static float onryou;

	public void OnclickStartButton()
	{
			onryou = slider.value;
			Option.SetActive(false);
			Home.SetActive(true);
		Debug.Log("音量変わった"+onryou);
			
	}
}
