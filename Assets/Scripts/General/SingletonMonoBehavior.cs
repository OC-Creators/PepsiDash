using UnityEngine;
using System;

namespace General
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected abstract bool dontDestroyOnLoad { get; }
        protected static T instance;
        public static T Instance
        {
            get
            {
                // nullチェック
                if (instance == null)
                {
                    // Tがアタッチされているゲームオブジェクトを探す
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

        protected virtual void Awake()
        {
            if (CheckInstance())
            {
                init();
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            
        }

        protected virtual void init()
        {

        }

        protected virtual bool CheckInstance()
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
            Destroy(this.gameObject);
            return false;
        }
    }
}