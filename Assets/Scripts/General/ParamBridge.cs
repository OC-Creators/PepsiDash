using UnityEngine;

namespace General
{
    public class ParamBridge : SingletonMonoBehaviour<ParamBridge>
    {
        public Param param;
        public JsonManager<Param> jm;

        protected override bool dontDestroyOnLoad { get { return true; } }

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

        public enum Signal
        {
            Stay,
            UpdateView,
            UpdateScreen
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

        void Start()
        {
            var param_json = $"{Application.dataPath}/Resources/Data/param.json";
            jm = new JsonManager<Param>(param_json);
            Debug.Log($"Import {param_json}");
            param = jm.Load();
        }

        void OnDestroy()
        {
            jm.Dump(param);
        }
    }
}
