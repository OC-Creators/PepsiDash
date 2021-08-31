using System;
using UnityEngine;

namespace General
{
    public class StartManager : ScreenManager<StartManager>
    {
        protected override void Start()
        {
            base.Start();
            if (pb.SMode == ScreenMode.Dummy)
            {
                pb.SMode = ScreenMode.Start;
            }
            
            pb.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}