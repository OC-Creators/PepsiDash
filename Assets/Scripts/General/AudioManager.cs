﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General {

    public class AudioManager : MonoBehaviour
    {
        public Slider bGMSlider;
        public Slider sESlider;
        private AudioSource bgm;
        private AudioSource se;

        // Start is called before the first frame update
        void Start()
        {
            // オーディオ管理
            var audio = this.GetComponents<AudioSource>();
            bgm = audio[0];
            se = audio[1];
            bGMSlider.onValueChanged.AddListener(value => bgm.volume = value);//ゲーム上のBGM音量と紐づけする
            sESlider.onValueChanged.AddListener(value => se.volume = value);//ゲーム上のSE音量と紐づけする

            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void PlayClick() {
            se.PlayOneShot(se.clip);
        }
    }
}