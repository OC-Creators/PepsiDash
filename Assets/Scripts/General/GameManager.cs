using System;
using UnityEngine;

namespace General {
    
    public class GameManager : ScreenManager<GameManager>
    {
        private bool stopTheWorld = false;
        private float elapsed = 0f;
        public float Elapsed
        {
            get { return elapsed; }
        }
        private bool isOver = false;
        public bool IsOver
        {
            get { return isOver; }
            set { isOver = value; }
        }

        private bool catched = false;
        public bool Catched
        {
            get { return catched; }
            set { catched = value; }
        }

        protected override void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.Game;
            }

            ParamBridge.UpdateView(ViewMode.InGame);
        }

        protected override void Update()
        {
            if (stopTheWorld)
            {
                Time.timeScale = 0f;
            }

            if (!isOver)
            {
                elapsed += Time.deltaTime;
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
                        Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
                        AudioManager.Instance.Replay();
                    }
                    break;

                case ViewMode.InGame:
                    // Pauseビューを非表示
                    if (curr == ViewMode.Pause)
                    {
                        Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
                        stopTheWorld = false;
                        Time.timeScale = 1f;
                    }
                    break;

                case ViewMode.GameEnd:
                    break;

                case ViewMode.Pause:
                    // Pauseビューを表示
                    if (curr == ViewMode.GameOption)
                    {
                        Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
                        Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
                    }
                    else if (curr == ViewMode.InGame)
                    {
                        Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
                        stopTheWorld = true;
                    }
                    break;

                case ViewMode.GameOption:
                    // Pauseビューを非表示にし、GameOptionビューを表示
                    if (curr == ViewMode.Pause)
                    {
                        Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
                        Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
                    }
                    break;

                case ViewMode.Result:
                    // Resultビューを表示
                    // GameEndを経由する場合はInGame -> GameEnd
                    if (curr == ViewMode.InGame)
                    {
                        isOver = true;
                        Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
                    }
                    break;
            }

            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}