using System;
using UnityEngine;
using UnityEngine.UI;
using General;

namespace UserInterface {
	
	public class UIManager : SingletonMonoBehaviour<UIManager> {
		protected override bool dontDestroyOnLoad { get { return false; } }
		public Text scoreText;
		public Text resultText;
		public Text resultScoreText;
		public Image image;
		private GameManager gm;
		private ParamBridge pb;
		// 初期化
		void Start ()
		{
			gm = GameManager.Instance;
			pb = ParamBridge.Instance;
		}

		// 更新
		void Update ()
		{
			if (gm.IsOver)
			{
				return;
			}

			var rest = 30 - (int)Math.Floor(gm.Elapsed);
			var bonus = 0;
			var score = rest + bonus;
			// (@miki) 将来的にアイテムボーナス（コインとか）を導入して加算する予定
			scoreText.text = "Score: -";
			image.fillAmount =  Math.Max(0f, 1f - gm.Elapsed / 30f);
			
			if (gm.Catched || gm.Reached || gm.Elapsed > 30f)
			{
				image.fillAmount = 0f;
				resultText.text = gm.Reached ? "Stage1 Clear!!" : "Stage1 Failed";
				// (@miki) ペプシの残り残量(最大30点) + ゲームクリアでボーナス20点
				resultScoreText.text = gm.Reached ? $"Score: {score += 20}" : $"Score: {score}";
				// ハイスコア更新
				pb.HighScore = score;
				// GameEndを経由する場合はResult -> GameEnd
				pb.UpdateView(ViewMode.Result);
				Debug.Log("Game is over");
			}
		}
	}
}