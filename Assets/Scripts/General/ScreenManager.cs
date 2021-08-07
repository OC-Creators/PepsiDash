using System;
using UnityEngine;

namespace General
{
    public abstract class ScreenManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] views;

        protected override bool dontDestroyOnLoad { get { return false; } }

        protected virtual void Start()
        {

        }
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
            Debug.Log($"prev: {ParamBridge.PrevVMode.ToStringQuickly()}, curr: {ParamBridge.VMode.ToStringQuickly()}, signal: {ParamBridge.UpdateSignal}");
        }

        protected virtual void SwitchView()
        {
            var curr = ParamBridge.PrevVMode;
            var next = ParamBridge.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
            Debug.Log($"switched {curr.ToStringQuickly()} to {next.ToStringQuickly()}");
        }

        protected virtual void SwitchScreen()
        {
            var sm = ParamBridge.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            ParamBridge.UpdateSignal = ParamBridge.Signal.Stay;
            Debug.Log($"changed to {sm}");
        }
    }
}