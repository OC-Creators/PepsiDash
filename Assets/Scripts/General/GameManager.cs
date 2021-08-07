using System;
using UnityEngine;

namespace General {
    
    public class GameManager : ScreenManager
    {
        private bool stopTheWorld = false;

        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.Game;
            }
            if (ParamBridge.VMode == ViewMode.Dummy)
            {
                ParamBridge.VMode = ViewMode.GameBegin;
            }
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }

        protected override void Update()
        {
            if (stopTheWorld)
            {
                Time.timeScale = 0f;
            }

            base.Update();
        }

        protected override void SwitchView()
        {
            var curr = ParamBridge.PrevVMode;
            var next = ParamBridge.VMode;

            switch (next)
            {
                case ViewMode.GameBegin:
                    if (curr == ViewMode.Result)
                    {
                        Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
                    }
                    ParamBridge.VMode = ViewMode.InGame;
                    break;

                case ViewMode.InGame:
                    // Pauseビューを非表示
                    if (curr == ViewMode.Pause)
                    {
                        Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
                        stopTheWorld = false;
                    }
                    break;

                case ViewMode.GameEnd:
                    ParamBridge.VMode = ViewMode.Result;
                    break;

                case ViewMode.Pause:
                    // Pauseビューを表示
                    Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
                    stopTheWorld = true;
                    break;

                case ViewMode.GameOption:
                    // Pauseビューを非表示にし、GameOptionビューを表示
                    Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
                    Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
                    break;

                case ViewMode.Result:
                    // Resultビューを表示
                    Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
                    break;
            }

            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}