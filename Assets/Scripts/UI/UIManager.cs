using System;
using UnityEngine;
using UnityEngine.UI;
using General;

namespace UserInterface {
	
	public class UIManager : SingletonMonoBehaviour<UIManager> {
		protected override bool dontDestroyOnLoad { get { return false; } }
		public Text scoreText;
		public Text resultText;
		public Image image;
		// 初期化
		void Start ()
		{
		
		}

		// 更新
		void Update ()
		{
			if (GameManager.Instance.IsOver)
			{
				return;
			}

			var score = Math.Floor(GameManager.Instance.Elapsed);
			scoreText.text = $"Score: {score}";
			/*if(GetWater.water){
				image.fillAmount =  1f;
				Debug.Log("水ゲットだぜ");
				GetWater.water = false;
			}else{
				image.fillAmount =  Math.Max(0f, 1f - GameManager.Instance.Elapsed / 30f);
			}*/
			image.fillAmount =  Math.Max(0f, 1f - GameManager.Instance.Elapsed / 30f);
			
			if (GameManager.Instance.Elapsed > 30f)
			{
				image.fillAmount = 0f;
				resultText.text = $"Score: {score}";
				GameManager.Instance.IsOver = true;
				// GameEndを経由する場合はResult -> GameEnd
				ParamBridge.UpdateView(ViewMode.Result);
				Debug.Log("Game is over");
			}
		}
	}
}