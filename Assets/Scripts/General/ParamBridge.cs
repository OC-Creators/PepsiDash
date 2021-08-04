using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class ParamBridge : MonoBehaviour
    {
        public static ViewMode prevVMode = ViewMode.Dummy;
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
                    updateSignal = Signal.UpdateView;
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
                    vmode = smode.GetEntryViewMode();
                    updateSignal = Signal.UpdateScreen;
                }
            }
        }

        public enum Signal
        {
            Stay,
            UpdateView,
            UpdateScreen
        }

        public static Signal updateSignal = Signal.Stay;
        public static ViewMode nextVMode = ViewMode.Dummy;

        public static void UpdateView(ViewMode vmode)
        {
            VMode = vmode;
        }

        public static void UpdateScreen(ScreenMode smode)
        {
            SMode = smode;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
