using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : ScreenManager<StageSelectManager>
    {
        private const string BGM_NAME = "lobby";
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
            am.Play(BGM_NAME);
        }


    }
}