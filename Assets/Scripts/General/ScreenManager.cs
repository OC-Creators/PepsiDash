using System;
using UnityEngine;

namespace General
{
    public abstract class ScreenManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        // legacy code
        [SerializeField] protected GameObject[] views;
        protected ParamBridge pb;
        protected AudioManager am;
        protected GameFlowController gfc;
        protected override bool dontDestroyOnLoad { get { return false; } }

        protected override void init()
        {
            pb = ParamBridge.Instance;
            am = AudioManager.Instance;
            gfc = GameFlowController.Instance;
        }
        protected override void Update()
        {

        }

    }
}