using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface{
	
	public class Menu_home : MonoBehaviour
	{
		public GameObject NotActive;
		public GameObject Active;
		public Slider slider;
		public static float volume;

		public void OnclickStartButton()
		{
				volume = slider.value;
				NotActive.SetActive(false);
				Active.SetActive(true);
			//Debug.Log("音量変わった"+onryou);
				
		}
	}
}