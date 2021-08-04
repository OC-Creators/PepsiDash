using System;
using UnityEngine;

namespace General
{
    public class StartManager : MonoBehaviour
    {
        public GameObject[] views;
        public ParamBridge bridge;

        void Start()
        {
            ParamBridge.SMode = ScreenMode.Start;
            ParamBridge.VMode = ViewMode.Title;
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }

        // ビュー切り替え処理
        void Update()
        {
            switch (ParamBridge.UpdateSignal)
            {
                case ParamBridge.Signal.Stay:
                    break;
                case ParamBridge.Signal.UpdateView:
                    SwitchView();
                    break;
                case ParamBridge.Signal.UpdateScreen:
                    SwitchScreen();
                    break;
            }
        }


        public void SwitchScreen()
        {
            var sm = ParamBridge.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            Debug.Log($"changed to {sm}");
        }

        public void SwitchView()
        {
            var curr = ParamBridge.PrevVMode;
            var next = ParamBridge.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
        }

    }
}