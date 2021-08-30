using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager<StageSelectManager>
    {
        protected override void init()
        {
            base.init();

            if (gfc.VMode != ViewMode.StageList)
            {
                gfc.VMode = ViewMode.StageList;
            }
            if (gfc.SMode != ScreenMode.StageSelect)
            {
                gfc.SMode = ScreenMode.StageSelect;
            }

            gfc.Views = views;

            AudioManager.Instance.PlayBGM("penguin");
        }


    }
}