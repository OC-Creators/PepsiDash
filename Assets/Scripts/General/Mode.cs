using UnityEngine;

namespace General {
	public enum ViewMode
	{
		Dummy = -1,
		Title,
		Credit,
		Option,
		StageList,
		Difficulty,
		HighScore,
		GameBegin,
		InGame,
		Result,
		Share,
		Pause,
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
				case ViewMode.Difficulty:
					return "DifficultyView";
				case ViewMode.HighScore:
					return "HighScoreView";
				case ViewMode.GameBegin:
					return "GameBeginView";
				case ViewMode.InGame:
					return "InGameView";
				case ViewMode.Result:
					return "ResultView";
				case ViewMode.Share:
					return "ShareView";
				case ViewMode.Pause:
					return "PauseView";
				case ViewMode.Option:
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
				case ScreenMode.Start:
					return "StartScene";
				case ScreenMode.StageSelect:
					return "StageSelectScene";
				case ScreenMode.Game:
					return "GameScene";
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
					return ViewMode.GameBegin;
				default:
					return ViewMode.Dummy;
			}
		}
	}
}