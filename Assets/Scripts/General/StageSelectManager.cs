using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager<StageSelectManager>
    {
        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.StageSelect;
            }
            
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;

            AudioManager.Instance.UpdateClip("penguin");
        }
    }
}