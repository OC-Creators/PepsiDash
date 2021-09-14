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
		// ゲーム画面
		GameEntry,
		InGame,
		Result,
		Share,
		Pause,
		GameOption,
		OpeningMovie,
		ResultMovie
	}

	public enum ScreenMode
	{
		Dummy = -1,
		Start,
		StageSelect,
		Game
	}

	public enum Result
	{
		Excellent,
		Nice,
		Bad
	}

	// アクションシグナル
	public enum Signal
	{
		Stay,
		Forward,
		Backward,
		ToOption,
		ToCredit,
		Restart,
		Pause,
		ToTitle,
		Share,
		Skip
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
                case ViewMode.GameEntry:
                    return "GameView";
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
                case ViewMode.OpeningMovie:
					return "GameView";
                case ViewMode.ResultMovie:
                    return "GameView";
                case ViewMode.Dummy:
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

		public static Signal Parse(string str)
		{
			switch (str)
			{
				case "Forward":
					return Signal.Forward;
				case "Backward":
					return Signal.Backward;
				case "ToOption":
					return Signal.ToOption;
				case "ToCredit":
					return Signal.ToCredit;
				case "Restart":
					return Signal.Restart;
				case "Pause":
					return Signal.Pause;
				case "ToTitle":
					return Signal.ToTitle;
				case "Share":
					return Signal.Share;
				case "Skip":
					return Signal.Skip;
				default:
					return Signal.Stay;
			}
		}

    }
}