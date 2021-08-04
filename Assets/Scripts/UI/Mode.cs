using UnityEngine;

public enum ViewMode
{
	Dummy = -1,
	Title,
	CopyRight,
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

public enum SceneMode
{
	Dummy = -1,
	Start,
	StageSelect,
	Game
}

public static class ModeHelper {
	public static string ToStringQuickly(this ViewMode m) {
		switch (m) {
		case ViewMode.Title:
			return "TitleView";
		case ViewMode.CopyRight:
			return "CopyRightView";
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
	public static string ToStringQuickly(this SceneMode m) {
		switch (m) {
		case SceneMode.Start:
			return "StartScene";
		case SceneMode.StageSelect:
			return "StageSelectScene";
		case SceneMode.Game:
			return "GameScene";
		default:
			Debug.LogWarning($"Configure a case '{m}' of ModeHelper.ToStringQuickly");
			return m.ToString();
		}
	}

	public static ViewMode GetEntryViewMode(this SceneMode m) {
		switch (m) {
		case SceneMode.Start:
			return ViewMode.Title;
		case SceneMode.StageSelect:
			return ViewMode.StageList;
		case SceneMode.Game:
			return ViewMode.GameBegin;
		default:
			return ViewMode.Dummy;
		}
	}
}