using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface{
	
	public class BgmManager : MonoBehaviour
	{
		public Slider slider;
		AudioSource audioSource;
		public bool DontDestroyEnabled = true;

		void Start()
		{
			audioSource = GetComponent<AudioSource>();
			slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
			if (DontDestroyEnabled) {
				// Sceneを遷移してもオブジェクトが消えないようにする
				DontDestroyOnLoad (this);
			}
		}
		// Update is called once per frame
		void Update () {

		}
	}
}