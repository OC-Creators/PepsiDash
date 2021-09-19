using UnityEngine;

namespace General
{
    public class ParamBridge : SingletonMonoBehaviour<ParamBridge>
    {
        private Param param;
        public JsonManager<Param> jm;

        protected override bool dontDestroyOnLoad { get { return true; } }
        // 時を止めたかどうか
        private bool stopTheWorld = false;
        public bool StopTheWorld
        {
            get { return stopTheWorld; }
            set { stopTheWorld = value; }
        }
        // 経過時間
        private float elapsed = 0f;
        public float Elapsed
        {
            get { return elapsed; }
            set { elapsed = value; }
        }
        // ゲーム終了したかどうか
        private bool isOver = false;
        public bool IsOver
        {
            get { return isOver; }
            set { isOver = value; }
        }
        // 警備員に捕まったかどうか
        private bool catched = false;
        public bool Catched
        {
            get { return catched; }
            set { catched = value; }
        }
        // ゴールに到達したかどうか
        private bool reached = false;
        public bool Reached
        {
            get { return reached; }
            set { reached = value; }
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

        protected override void Awake()
        {
            if (CheckInstance())
            {
                var param_json = $"{Application.dataPath}/Resources/Data/param.json";
                jm = new JsonManager<Param>(param_json);
                Debug.Log($"Import {param_json}");
                param = new Param();
                jm.Load(ref param);
                highScore = param.high_score;
                bgmVolume = param.bgm_volume;
                seVolume = param.se_volume;
            }
        }

        void OnDestroy()
        {
            if (param is null) return;

            param.high_score = highScore;
            param.bgm_volume = bgmVolume;
            param.se_volume = seVolume;
            param.unlock = 0;

            jm.Dump(ref param);
        }
    }
}
