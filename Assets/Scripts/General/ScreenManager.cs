using System;
using UnityEngine;

namespace General
{
    public abstract class ScreenManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        [SerializeField] protected GameObject[] views;
        protected ParamBridge pb;
        protected AudioManager am;
        protected GameFlowController gfc;
        protected override bool dontDestroyOnLoad { get { return false; } }

        protected override void Start()
        {
            pb = ParamBridge.Instance;
            am = AudioManager.Instance;
            gfc = GameFlowController.Instance;
        }
        protected override void Update()
        {

        }
        // AudioManager.PlaySE()のラッパー関数
        public void PlaySE(AudioClip clip)
        {
            am.PlaySE(clip);
        }
        // GameFlowController.dispatch()のラッパー関数
        public void dispatch(string signal)
        {
            gfc.dispatch(signal);
        }
    }
}