using System;
using UnityEngine;

namespace General
{
    public abstract class ScreenManager : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] views;
        // Update is called once per frame
        protected virtual void Update()
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

        protected virtual void SwitchView()
        {
            var curr = ParamBridge.PrevVMode;
            var next = ParamBridge.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }

        protected virtual void SwitchScreen()
        {
            var sm = ParamBridge.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            Debug.Log($"changed to {sm}");
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
        }
    }
}