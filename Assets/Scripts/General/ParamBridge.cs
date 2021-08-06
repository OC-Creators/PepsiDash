using UnityEngine;

namespace General
{
    public class ParamBridge : SingletonMonoBehaviour<ParamBridge>
    {
        public Param param;
        public JsonManager<Param> jm;

        protected override bool dontDestroyOnLoad { get { return true; } }

        // 画面モード
        private static ViewMode prevVMode = ViewMode.Dummy;
        private static ViewMode vmode = ViewMode.Dummy;
        public static ViewMode VMode
        {
            get { return vmode; }
            set
            {
                if (vmode != value)
                {
                    prevVMode = vmode;
                    vmode = value;
                    _updateSignal = Signal.UpdateView;
                    // Debug.Log($"prev: {prevVMode.ToStringQuickly()}, curr: {vmode.ToStringQuickly()}, signal: {_updateSignal}");
                }
            }
        }

        public static ViewMode PrevVMode
        {
            get { return prevVMode; }
        }

        // シーンモード
        private static ScreenMode smode = ScreenMode.Dummy;
        public static ScreenMode SMode
        {
            get { return smode; }
            set
            {
                if (smode != value)
                {
                    smode = value;
                    prevVMode = vmode;
                    vmode = smode.GetEntryViewMode();
                    _updateSignal = Signal.UpdateScreen;
                    // Debug.Log($"smode: {smode.ToStringQuickly()}, vmode: {vmode.ToStringQuickly()}, signal: {_updateSignal}");
                }
            }
        }

        // 遷移シグナル
        public enum Signal
        {
            Stay,
            UpdateView,
            UpdateScreen
        }

        // スコア
        public static int score = 0;
        public static int Score
        {
            get { return score; }
            set { score = value; }
        }

        // BGM音量
        public static float bgmVolume = 0;
        public static float BGMVolume
        {
            get { return bgmVolume; }
            set { bgmVolume = value; }
        }

        // SE音量
        public static float seVolume = 0;
        public static float SEVolume
        {
            get { return seVolume; }
            set { seVolume = value; }
        }

        private static Signal _updateSignal = Signal.Stay;
        public static Signal UpdateSignal
        {
            get { return _updateSignal; }
            set { _updateSignal = value; }
        }

        public static void UpdateView(ViewMode vmode)
        {
            VMode = vmode;
        }

        public static void UpdateScreen(ScreenMode smode)
        {
            SMode = smode;
        }

        void Awake()
        {
            base.Awake();
            //var param_json = $"{Application.dataPath}/Resources/Data/param.json";
            //jm = new JsonManager<Param>(param_json);
            //Debug.Log($"Import {param_json}");
            //param = new Param();
            //jm.Load(ref param);
            //score = param.score;
            //bgmVolume = param.bgm_volume;
            //seVolume = param.se_volume;
            //jm.Dump(ref param);
            score = 0;
            bgmVolume = 1.0f;
            seVolume = 1.0f;
        }

        void OnDestroy()
        {
            //param = new Param
            //{
            //    bgm_volume = AudioManager.Instance.BGMVolume,
            //    se_volume = AudioManager.Instance.SEVolume,
            //    score = Score,
            //    unlock = 0
            //};

            //jm.Dump(ref param);
        }
    }
}
