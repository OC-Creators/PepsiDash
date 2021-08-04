using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface{
	
	public class Clear : MonoBehaviour
	{
		public GameObject Clearpanel;

		//public ParticleSystem particle;

		public Text clearText;

		//ゲーム開始時に実行される
		void Start()
		{
			//パネルを隠す
			Clearpanel.SetActive(false);
		}

		//クリアパネルを表示させる
		//GoalからSendMessageで呼ばれる
		void OnEnter()
		{
			//パネルを表示させる
			Clearpanel.SetActive(true);
			//スコアをPlayerPrefsから取得する
			int nowScore = PlayerPrefs.GetInt("score", 0);

			Debug.Log(nowScore);
			//スコアをテキストエリアに表示する
			clearText.text = nowScore.ToString();
			//パーティクルを再生する
			//particle.Play();
		}
	}
}