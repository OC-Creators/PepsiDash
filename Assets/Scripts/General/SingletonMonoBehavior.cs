using UnityEngine;
using System;

namespace General
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected abstract bool dontDestroyOnLoad { get; }
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = FindObjectOfType(t) as T;
                    if (instance == null)
                    {
                        Debug.LogError($"No GameObject to which {t} is attached.");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            // 他のゲームオブジェクトにアタッチされているか調べる
            // アタッチされている場合は破棄する。
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(this);
                }
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }
            Destroy(this);
            return false;
        }
    }
}