using UnityEngine;

namespace General {
	public enum ViewMode
	{
		// ダミー
		Dummy = -1,
		// Start画面
		Title,
		Credit,
		StartOption,
		// ステージ選択画面
		StageList,
		LevelList,
		HighScore,
		// ゲーム画面
		GameEntry,
		InGame,
		Result,
		Share,
		Pause,
		GameOption
	}

	public enum ScreenMode
	{
		Dummy = -1,
		Start,
		StageSelect,
		Game
	}

	public static class ModeHelper
	{
		public static string ToStringQuickly(this ViewMode m)
		{
			switch (m)
			{
				case ViewMode.Title:
					return "TitleView";
				case ViewMode.Credit:
					return "CreditView";
				case ViewMode.StageList:
					return "StageListView";
				case ViewMode.LevelList:
					return "LevelListView";
				case ViewMode.InGame:
					return "GameView";
				case ViewMode.Result:
					return "ResultView";
				case ViewMode.Share:
					return "ShareView";
				case ViewMode.Pause:
					return "PauseView";
				case ViewMode.StartOption:
					return "OptionView";
				case ViewMode.GameOption:
					return "OptionView";
				default:
					Debug.LogWarning($"Configure a case '{m}' of ModeHelper.ToStringQuickly");
					return m.ToString();
			}
		}
		public static string ToStringQuickly(this ScreenMode m)
		{
			switch (m)
			{
				case ScreenMode.Dummy:
					return "Dummy";
				case ScreenMode.Start:
					return "StartScreen";
				case ScreenMode.StageSelect:
					return "StageSelectScreen";
				case ScreenMode.Game:
					return "IntegratedGameScreen";
				default:
					Debug.LogWarning($"Configure a case '{m}' of ModeHelper.ToStringQuickly");
					return m.ToString();
			}
		}

		public static ViewMode GetEntryViewMode(this ScreenMode m)
		{
			switch (m)
			{
				case ScreenMode.Start:
					return ViewMode.Title;
				case ScreenMode.StageSelect:
					return ViewMode.StageList;
				case ScreenMode.Game:
					return ViewMode.GameEntry;
				default:
					return ViewMode.Dummy;
			}
		}
	}
}