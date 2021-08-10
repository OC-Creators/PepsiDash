﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General {

    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        public Slider bGMSlider;
        public Slider sESlider;
        public List<AudioClip> bGMClip;
        private AudioSource source;
        private float seVolume = 1f;

        protected override bool dontDestroyOnLoad { get { return true; } }

        public AudioSource Source
        {
            get { return source; }
            set { source = value; }
        }

        public float SEVolume
        {
            get { return seVolume; }
            set { seVolume = value; }
        }

        // Start is called before the first frame update
        protected override void Awake()
        {
            if (CheckInstance())
            {
                initSource();
                source.Play();
            }
        }

        protected override bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as AudioManager;
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

            bGMClip.ForEach(clip =>
            {
                if (!AudioManager.Instance.bGMClip.Contains(clip))
                {
                    AudioManager.Instance.bGMClip.Add(clip);
                }
            });
            Destroy(gameObject);
            return false;
        }

        public void initSource()
        {
            // オーディオ管理
            Debug.Assert(bGMClip[0] != null, $"BGMClip is null");
            source = gameObject.AddComponent<AudioSource>();
            source.clip = bGMClip[0];
            source.volume = ParamBridge.bgmVolume;
            source.loop = true;
            seVolume = ParamBridge.SEVolume;
            //ゲーム上の音量と紐づけする
            if (bGMSlider != null) 
            {
                bGMSlider.value = source.volume;
                bGMSlider.onValueChanged.AddListener(value => source.volume = value);
            }
            if (sESlider != null) 
            {
                sESlider.value = source.volume;
                sESlider.onValueChanged.AddListener(value => seVolume = value);
            }

            Debug.Log("AudioManager: Initialized");
        }

        public void UpdateClip(string clipName)
        {
            var newClip = bGMClip.Find(clip => clip.name == clipName);
            if (newClip != null)
            {
                source.clip = newClip;
                source.Play();
            }
            else
            {
                Debug.Log($"No such BGM clip `{clipName}'");
            }
        }

        public void Replay()
        {
            source.Stop();
            source.Play();
        }

        public void PlayClick(AudioClip clip)
        {
            Debug.Assert(clip != null, $"{clip} is null");
            source.PlayOneShot(clip, seVolume);
        }
    }
}