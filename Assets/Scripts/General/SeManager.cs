using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface{
	public class SeManager : MonoBehaviour
	{
		public Slider slider;
		AudioSource audioSource;
		//public static volume; 

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
			slider.onValueChanged.AddListener(value => this.audioSource.volume = value);//ゲーム上の音量と紐づけする

			//Debug.Log("音量が変更されました");

		}
		// Update is called once per frame
		void Update () {
			//Debug.Log("音量が変更されました");
		}
	}
}