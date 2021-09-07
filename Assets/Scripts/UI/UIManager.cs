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
		private GameFlowController gfc;
		private ParamBridge pb;
		// 初期化
		void Start ()
		{
			gfc = GameFlowController.Instance;
			pb = ParamBridge.Instance;
		}

		// 更新
		protected override void Update ()
		{
			if (pb.IsOver)
			{
				return;
			}

			var rest = 30 - (int)Math.Floor(pb.Elapsed);
			var bonus = 0;
			var score = rest + bonus;
			// (@miki) 将来的にアイテムボーナス（コインとか）を導入して加算する予定
			scoreText.text = "Score: -";
			image.fillAmount =  Math.Max(0f, 1f - pb.Elapsed / 30f);
			
			if (pb.Catched || pb.Reached || pb.Elapsed > 30f)
			{
				image.fillAmount = 0f;
				resultText.text = pb.Reached ? "Stage1 Clear!!" : "Stage1 Failed";
				// (@miki) ペプシの残り残量(最大30点) + ゲームクリアでボーナス20点
				resultScoreText.text = pb.Reached ? $"Score: {score += 20}" : $"Score: {score}";
				// ハイスコア更新
				pb.HighScore = score;
				// GameEndを経由する場合はResult -> GameEnd
				gfc.dispatch(Signal.Forward);
				Debug.Log("Game is over");
			}
		}
	}
}