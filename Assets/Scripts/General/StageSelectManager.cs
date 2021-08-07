using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager
    {
        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.StageSelect;
            }
            if (ParamBridge.VMode == ViewMode.Dummy)
            {
                ParamBridge.VMode = ViewMode.StageList;
            }
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;

            AudioManager.Instance.UpdateClip("penguin");
        }
    }
}