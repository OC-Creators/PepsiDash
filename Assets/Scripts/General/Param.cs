using System;

namespace General {
    [Serializable]
    public class Param
    {
        public ViewMode prev_vmode;
        public ViewMode vmode;
        public ScreenMode smode;
        public ParamBridge.Signal update_signal;
        public float bgm_volume;
        public float se_volume;
    }
}