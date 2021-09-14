using System;
using System.Collections;
using UnityEngine;

namespace General
{
    public class GameFlowController : SingletonMonoBehaviour<GameFlowController>
    {
        protected override bool dontDestroyOnLoad { get { return true;} }
        protected ParamBridge pb;
        protected AudioManager am;

        private GameObject[] views;
        public GameObject[] Views
        {
            get { return views; }
            set { views = value; }
        }

        // 画面モード
        [SerializeField] protected ViewMode vmode = ViewMode.Dummy;
        public ViewMode VMode
        {
            get { return vmode; }
            set { vmode = value; }
        }

        // シーンモード
        [SerializeField] protected ScreenMode smode = ScreenMode.Dummy;
        public ScreenMode SMode
        {
            get { return smode; }
            set { smode = value; }
        }

        [SerializeField] protected Signal actionSignal = Signal.Stay;
        public void dispatch(string signal)
        {
            actionSignal = (Signal)Enum.Parse(typeof(Signal), signal, true);
        }

        public void dispatch(Signal signal)
        {
            actionSignal = signal;
        }


        protected override void init()
        {
            pb = ParamBridge.Instance;
            am = AudioManager.Instance;
        }

        public virtual void SwitchView(ViewMode next, bool nextActive = true, bool currActive = false)
        {
            // nextActiveがtrue（デフォルト）ならSetActiveする
            if (nextActive)
            {
                Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
            }
            // currActiveがfalse（デフォルト）ならSetActiveする
            if (!currActive)
            {
                Array.Find(views, v => v.name == vmode.ToStringQuickly())?.SetActive(false);
            }
            actionSignal = Signal.Stay;
            vmode = next;
            // Debug.Log($"switched {curr.ToStringQuickly()} to {next.ToStringQuickly()}");
        }

        public virtual void SwitchScreenFade(ScreenMode next)
        {
            FadeManager.Instance.LoadScene(next.ToStringQuickly(), 1.0f);
            actionSignal = Signal.Stay;
            vmode = next.GetEntryViewMode();
            smode = next;
            // Debug.Log($"changed to {sm}");
        }
        public virtual void SwitchScreen(ScreenMode next)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(next.ToStringQuickly());
            actionSignal = Signal.Stay;
            vmode = next.GetEntryViewMode();
            smode = next;
            // Debug.Log($"changed to {sm}");
        }

        protected override void Update()
        {
            if (actionSignal == Signal.Stay) return;

            switch (vmode)
            {
                // タイトル画面
                case ViewMode.Title:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            SwitchScreenFade(ScreenMode.StageSelect);
                            break;
                        case Signal.ToOption:
                            SwitchView(ViewMode.StartOption);
                            break;
                        case Signal.ToCredit:
                            SwitchView(ViewMode.Credit);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // オプション画面（スタートシーン）
                case ViewMode.StartOption:
                    switch (actionSignal)
                    {
                        case Signal.Backward:
                            SwitchView(ViewMode.Title);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // クレジット画面
                case ViewMode.Credit:
                    switch (actionSignal)
                    {
                        case Signal.Backward:
                            SwitchView(ViewMode.Title);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // ステージ選択画面
                case ViewMode.StageList:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            // 難易度選択画面 -> 未実装
                            // SwitchView(ViewMode.LevelList);
                            SwitchScreen(ScreenMode.Game);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // 難易度選択画面（未実装）
                case ViewMode.LevelList:
                    // switch (actionSignal)
                    // {
                    //     case Signal.Forward:
                    //         SwitchScreen(ScreenMode.Game);
                    //         break;
                    //     case Signal.Backward:
                    //         SwitchView(ViewMode.StageList);
                    //         break;
                    //     default:
                    //         Debug.LogError($"Signal {actionSignal} is not allowed.");
                    //         break;
                    // }
                    // break;
                // ゲーム画面エントリポイント
                case ViewMode.GameEntry:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            SwitchView(ViewMode.OpeningMovie, nextActive: false, currActive: true);
                            StartCoroutine(GameManager.Instance.PlayOpeningMovie());
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // オープニングムービー
                case ViewMode.OpeningMovie:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            SwitchView(ViewMode.InGame, nextActive: false, currActive: true);
                            break;
                        // スキップ
                        case Signal.Skip:
                            //StopCoroutine(op);
                            //SwitchView(ViewMode.InGame);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // ゲーム中画面
                case ViewMode.InGame:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            pb.IsOver = true;
                            SwitchView(ViewMode.ResultMovie, nextActive: false, currActive: true);
                            StartCoroutine(GameManager.Instance.PlayResultMovie(Result.Excellent));
                            break;
                        case Signal.Pause:
                            SwitchView(ViewMode.Pause, currActive: true);
                            pb.StopTheWorld = true;
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // リザルトムービー
                case ViewMode.ResultMovie:
                    switch (actionSignal)
                    {
                        case Signal.Forward:
                            SwitchView(ViewMode.Result, currActive: true);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // リザルト画面
                case ViewMode.Result:
                    switch (actionSignal)
                    {
                        case Signal.Restart:
                            SwitchView(ViewMode.GameEntry, nextActive: false);
                            pb.IsOver = false;
                            pb.Elapsed = 0f;
                            break;
                        case Signal.ToTitle:
                            SwitchScreenFade(ScreenMode.Start);
                            pb.IsOver = false;
                            break;
                        case Signal.Share:
                            var url = $"https://twitter.com/intent/tweet?text=今回の記録は『{pb.HighScore}』点";
							Application.OpenURL(url);
                            actionSignal = Signal.Stay;
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // ポーズ画面
                case ViewMode.Pause:
                    switch (actionSignal)
                    {
                        case Signal.Backward:
                            SwitchView(ViewMode.InGame, nextActive: false);
                            pb.StopTheWorld = false;
                            Time.timeScale = 1f;
                            break;
                        case Signal.ToOption:
                            SwitchView(ViewMode.GameOption);
                            break;
                        case Signal.Restart:
                            SwitchView(ViewMode.GameEntry, nextActive: false);
                            pb.Elapsed = 0f;
                            break;
                        case Signal.ToTitle:
                            SwitchScreenFade(ScreenMode.Start);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // オプション画面（ゲームシーン）
                case ViewMode.GameOption:
                    switch (actionSignal)
                    {
                        case Signal.Backward:
                            SwitchView(ViewMode.Pause);
                            break;
                        default:
                            Debug.LogError($"Signal {actionSignal} is not allowed.");
                            break;
                    }
                    break;
                // その他
                default:
                    Debug.LogError($"ViewMode {vmode} is now not supported, so add it to 'GameFlowController.Update()'.");
                    break;
            }
        }


    }
    


}
