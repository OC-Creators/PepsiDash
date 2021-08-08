using System;
using UnityEngine;

namespace General
{
    public class StartManager : ScreenManager<StartManager>
    {
        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.Start;
            }
            
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}