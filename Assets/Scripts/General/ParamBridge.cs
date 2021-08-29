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
                prevVMode = vmode;
                vmode = value;
                updateSignal = Signal.UpdateView;
                Debug.Log($"prev: {prevVMode.ToStringQuickly()}, curr: {vmode.ToStringQuickly()}, signal: {updateSignal}");
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
                smode = value;
                prevVMode = vmode;
                vmode = smode.GetEntryViewMode();
                updateSignal = Signal.UpdateScreen;
                Debug.Log($"smode: {smode.ToStringQuickly()}, vmode: {vmode.ToStringQuickly()}, signal: {updateSignal}");
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
        public static int highScore = 0;
        public static int HighScore
        {
            get { return highScore; }
            set 
            { 
                if (value > highScore)
                {
                    highScore = value; }
                } 
            }

        // BGM音量
        public static float bgmVolume = 1f;
        public static float BGMVolume
        {
            get { return bgmVolume; }
            set { bgmVolume = value; }
        }

        // SE音量
        public static float seVolume = 1f;
        public static float SEVolume
        {
            get { return seVolume; }
            set { seVolume = value; }
        }

        private static Signal updateSignal = Signal.Stay;
        public static Signal UpdateSignal
        {
            get { return updateSignal; }
            set { updateSignal = value; }
        }

        public static void UpdateView(ViewMode vmode)
        {
            VMode = vmode;
        }

        public static void UpdateScreen(ScreenMode smode)
        {
            SMode = smode;
        }

        protected override void Awake()
        {
            base.Awake();
            var param_json = $"{Application.dataPath}/Resources/Data/param.json";
            jm = new JsonManager<Param>(param_json);
            Debug.Log($"Import {param_json}");
            // param = new Param();
            jm.Load(ref param);
            highScore = param.high_score;
            bgmVolume = param.bgm_volume;
            seVolume = param.se_volume;
        }

        void OnDestroy()
        {
            param = new Param
            {
               bgm_volume = bgmVolume,
               se_volume = seVolume,
               high_score = highScore,
               unlock = 0
            };

            jm.Dump(ref param);
        }
    }
}
