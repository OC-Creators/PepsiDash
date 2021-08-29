using UnityEngine;

namespace General
{
    public class ParamBridge : SingletonMonoBehaviour<ParamBridge>
    {
        private Param param;
        public JsonManager<Param> jm;

        protected override bool dontDestroyOnLoad { get { return true; } }

        // 画面モード
        [SerializeField] private ViewMode prevVMode = ViewMode.Dummy;
        [SerializeField] private ViewMode vmode = ViewMode.Dummy;
        public ViewMode VMode
        {
            get { return vmode; }
            set
            {
                prevVMode = vmode;
                vmode = value;
                updateSignal = Signal.UpdateView;
                // Debug.Log($"prev: {prevVMode.ToStringQuickly()}, curr: {vmode.ToStringQuickly()}, signal: {updateSignal}");
            }
        }

        public ViewMode PrevVMode
        {
            get { return prevVMode; }
        }

        // シーンモード
        [SerializeField] private ScreenMode smode = ScreenMode.Dummy;
        public ScreenMode SMode
        {
            get { return smode; }
            set
            {
                smode = value;
                prevVMode = vmode;
                vmode = smode.GetEntryViewMode();
                updateSignal = Signal.UpdateScreen;
                // Debug.Log($"smode: {smode.ToStringQuickly()}, vmode: {vmode.ToStringQuickly()}, signal: {updateSignal}");
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
        [SerializeField] private int highScore = 0;
        public int HighScore
        {
            get { return highScore; }
            set 
            { 
                if (value > highScore)
                {
                    highScore = value;
                }
            } 
        }

        // BGM音量
        [SerializeField] private float bgmVolume = 1f;
        public float BGMVolume
        {
            get { return bgmVolume; }
            set { bgmVolume = value; }
        }

        // SE音量
        [SerializeField] private float seVolume = 1f;
        public float SEVolume
        {
            get { return seVolume; }
            set { seVolume = value; }
        }

        [SerializeField] private Signal updateSignal = Signal.Stay;
        public Signal UpdateSignal
        {
            get { return updateSignal; }
            set { updateSignal = value; }
        }

        public void UpdateView(ViewMode vmode)
        {
            VMode = vmode;
        }

        public void UpdateScreen(ScreenMode smode)
        {
            SMode = smode;
        }

        protected override void Awake()
        {
            base.Awake();
            var param_json = $"{Application.dataPath}/Resources/Data/param.json";
            jm = new JsonManager<Param>(param_json);
            Debug.Log($"Import {param_json}");
            param = new Param();
            jm.Load(ref param);
            highScore = param.high_score;
            bgmVolume = param.bgm_volume;
            seVolume = param.se_volume;
        }

        void OnDestroy()
        {
            param.high_score = highScore;
            param.bgm_volume = bgmVolume;
            param.se_volume = seVolume;
            param.unlock = 0;

            jm.Dump(ref param);
        }
    }
}
