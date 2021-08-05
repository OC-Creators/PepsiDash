using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General {
    
    public class GameManager : ScreenManager
    {
        public Camera cam;

        void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.Game;
            }
            if (ParamBridge.VMode == ViewMode.Dummy)
            {
                ParamBridge.VMode = ViewMode.InGame;
            }
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;

            // var cam = Camera.main;

            cam.ScreenToWorldPoint()
        }
    }
}