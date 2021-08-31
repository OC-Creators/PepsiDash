using System;
using UnityEngine;

namespace General
{
    public abstract class ScreenManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        [SerializeField] protected GameObject[] views;
        protected ParamBridge pb;

        protected override bool dontDestroyOnLoad { get { return false; } }

        protected virtual void Start()
        {
            pb = ParamBridge.Instance;
        }
        protected virtual void Update()
        {
            switch (pb.UpdateSignal)
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
            // Debug.Log($"prev: {pb.PrevVMode.ToStringQuickly()}, curr: {pb.VMode.ToStringQuickly()}, signal: {pb.UpdateSignal}");
        }

        protected virtual void SwitchView()
        {
            var curr = pb.PrevVMode;
            var next = pb.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly())?.SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly())?.SetActive(false);
            pb.UpdateSignal = ParamBridge.Signal.Stay;
            // Debug.Log($"switched {curr.ToStringQuickly()} to {next.ToStringQuickly()}");
        }

        protected virtual void SwitchScreen()
        {
            var sm = pb.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            pb.UpdateSignal = ParamBridge.Signal.Stay;
            // Debug.Log($"changed to {sm}");
        }
    }
}