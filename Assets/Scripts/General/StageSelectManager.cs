using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager<StageSelectManager>
    {
        protected override void Start()
        {
            base.Start();

            if (gfc.VMode != ViewMode.StageList)
            {
                gfc.VMode = ViewMode.StageList;
            }
            if (gfc.SMode != ScreenMode.StageSelect)
            {
                gfc.SMode = ScreenMode.StageSelect;
            }

            gfc.Views = views;

            am?.PlayBGM("penguin");
        }


    }
}