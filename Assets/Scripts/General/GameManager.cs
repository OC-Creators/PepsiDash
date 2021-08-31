using System;
using UnityEngine;

namespace General {
    
    public class GameManager : ScreenManager<GameManager>
    {
        protected override void Start()
        {
            base.Start();

            if (gfc.VMode != ViewMode.GameEntry)
            {
                gfc.VMode = ViewMode.GameEntry;
            }
            if (gfc.SMode != ScreenMode.Game)
            {
                gfc.SMode = ScreenMode.Game;
            }

            gfc.Views = views;

            pb.Elapsed = 0f;
            gfc.dispatch(Signal.Forward);
        }

        protected override void Update()
        {
            if (pb.StopTheWorld)
            {
                Time.timeScale = 0f;
            }

            if (!pb.IsOver)
            {
                pb.Elapsed += Time.deltaTime;
            }
        }
    }
}