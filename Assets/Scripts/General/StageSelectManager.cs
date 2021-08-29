using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager<StageSelectManager>
    {
        protected override void Start()
        {
            base.Start();
            if (pb.SMode == ScreenMode.Dummy)
            {
                pb.SMode = ScreenMode.StageSelect;
            }
            
            pb.UpdateSignal = ParamBridge.Signal.Stay;

            AudioManager.Instance.UpdateClip("penguin");
        }
    }
}