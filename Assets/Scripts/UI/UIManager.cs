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

		private int prevFrameCount = ParamBridge.INIT_FRAME;
		private float prevElapsed = ParamBridge.INIT_ELAPSED;
		private float elapsed = 0f;
		private int fps = 0;

		// 何秒ごとに温度値を消費するか
		private const float SPENDING_TEMP_RATE = 2f;
		// 消費温度値
		private const int SPENDING_TEMP_VALUE = 1;
		// 消費炭酸値
		private const int SPENDING_GAS_VALUE = 20;
		// Excellentになるための境界値
		private const int EXCELLENT_TEMP = 80;
		private const int EXCELLENT_GAS = 80;
		// Niceになるための境界値
		private const int NICE_TEMP = 30;
		private const int NICE_GAS = 30;
		

		// 初期化
		protected override void Start()
		{
			gfc = GameFlowController.Instance;
			pb = ParamBridge.Instance;
		}

		// 更新
		protected override void Update()
		{
			if (gfc.VMode < ViewMode.GameEntry || pb.IsOver)
            {
				return;
            }

			// TODO: 虚空使ったら gas -= 20, 使用中に減るように

			elapsed = pb.Elapsed - prevElapsed;
			// 時間経過で温度値減少
			if (elapsed >= SPENDING_TEMP_RATE)
			{
				pb.Temp -= SPENDING_TEMP_VALUE;
				prevElapsed = pb.Elapsed;
				elapsed = 0f;
			}
			// UI更新
			scoreText.text = $"ELAPSED={(int)pb.Elapsed}, TEMP={pb.Temp}, GAS={pb.Gas}";
			image.fillAmount =  Math.Max(ParamBridge.TEMP_MIN, (float)pb.Temp / ParamBridge.TEMP_MAX);

			// リザルト集計
			if (pb.Catched || pb.Reached || pb.Elapsed > ParamBridge.LIMIT_ELAPSED)
			{
				image.fillAmount = ParamBridge.TEMP_MIN;

				// スコア: Bad
				if (pb.Catched || !pb.Reached || pb.Temp < NICE_TEMP || pb.Gas < NICE_GAS)
                {
					// リザルト表示テキスト
					resultText.text = "Bad";
					// ハイスコア更新
					pb.HighScore = Result.Bad;
				}
				// スコア: Nice
				else if (pb.Temp < EXCELLENT_TEMP || pb.Gas < EXCELLENT_GAS)
                {
					// リザルト表示テキスト
					resultText.text = "Nice!";
					// ハイスコア更新
					pb.HighScore = Result.Nice;
				}
				// スコア: Excellent
				else
                {
					// リザルト表示テキスト
					resultText.text = "Excellent!!";
					// ハイスコア更新
					pb.HighScore = Result.Excellent;
				}
				
				gfc.dispatch(Signal.Forward);
				Debug.Log("Game is over");
			}
		}
	}
}