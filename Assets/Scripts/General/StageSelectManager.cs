using System;
using UnityEngine;

namespace General
{
    public class StageSelectManager : MonoBehaviour
    {
        public GameObject[] views;
        public ParamBridge bridge;
        // Start is called before the first frame update
        void Start()
        {
            if (ParamBridge.SMode == ScreenMode.Dummy)
            {
                ParamBridge.SMode = ScreenMode.StageSelect;
            }
            if (ParamBridge.VMode == ViewMode.Dummy)
            {
                ParamBridge.VMode = ViewMode.StageList;
            }
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }

        // Update is called once per frame
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
            // Debug.Log($"prev: {ParamBridge.PrevVMode.ToStringQuickly()}, curr: {ParamBridge.VMode.ToStringQuickly()}, signal: {ParamBridge.UpdateSignal}");
        }

        public void SwitchView()
        {
            var curr = ParamBridge.PrevVMode;
            var next = ParamBridge.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }

        public void SwitchScreen()
        {
            var sm = ParamBridge.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            Debug.Log($"changed to {sm}");
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
        
    }
}