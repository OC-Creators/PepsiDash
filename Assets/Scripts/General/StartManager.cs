using System;
using UnityEngine;

namespace General
{
    public class StartManager : ScreenManager
    {
        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.Start;
            }
            if (ParamBridge.VMode == ViewMode.Dummy)
            {
                ParamBridge.VMode = ViewMode.Title;
            }
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}