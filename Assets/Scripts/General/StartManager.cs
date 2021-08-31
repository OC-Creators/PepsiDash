using System;
using UnityEngine;

namespace General
{
    public class StartManager : ScreenManager<StartManager>
    {
        protected override void Start()
        {
            base.Start();

            if (gfc.VMode != ViewMode.Title)
            {
                gfc.VMode = ViewMode.Title;
            }
            if (gfc.SMode != ScreenMode.Start)
            {
                gfc.SMode = ScreenMode.Start;
            }

            gfc.Views = views;
        }

    }
}