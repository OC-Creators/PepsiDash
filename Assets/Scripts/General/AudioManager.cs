using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General {

    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        public Slider bGMSlider;
        public Slider sESlider;
        private AudioSource bgm;
        private AudioSource se;

        protected override bool dontDestroyOnLoad { get { return true; } }

        // Start is called before the first frame update
        void Start()
        {
            // オーディオ管理
            var audio = this.GetComponents<AudioSource>();
            this.bgm = audio[0];
            this.se = audio[1];
            bGMSlider.onValueChanged.AddListener(value => this.bgm.volume = value);//ゲーム上のBGM音量と紐づけする
            sESlider.onValueChanged.AddListener(value => this.se.volume = value);//ゲーム上のSE音量と紐づけする
        }

        public void PlayClick()
        {
            this.se.PlayOneShot(this.se.clip);
        }

        public void changeBGM(AudioSource audio)
        {
            this.bgm = audio;
        }
    }
}