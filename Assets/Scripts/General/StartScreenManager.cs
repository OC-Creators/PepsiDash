using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace General
{
    public class StartScreenManager : MonoBehaviour
    {
        public GameObject[] views;

        public Slider bGMSlider;
        public Slider sESlider;
        public GameObject bGM;
        public GameObject sE;
        public bool DontDestroyEnabled = true;

        private bool updateSignal = false;

        void Start()
        {
            // オーディオ管理
            sESlider.onValueChanged.AddListener(value => sE.GetComponent<AudioSource>().volume = value);//ゲーム上のSE音量と紐づけする
            bGMSlider.onValueChanged.AddListener(value => bGM.GetComponent<AudioSource>().volume = value);//ゲーム上のBGM音量と紐づけする

            if (DontDestroyEnabled)
            {
                // Sceneを遷移してもオブジェクトが消えないようにする
                DontDestroyOnLoad(this);
            }

            ParamBridge.SMode = ScreenMode.Start;
            ParamBridge.VMode = ViewMode.Title;
            ParamBridge.updateSignal = ParamBridge.Signal.Stay;
        }

        // ビュー切り替え処理
        void Update()
        {
            //switch (b.tag)
            //{
            //    case ViewMode.Title:

            //}
            //if (btn.name == "StartButton") FadeManager.Instance.LoadScene("StageSelectScreen", 1.0f);
            //if (btn.name == "Stage1Button") FadeManager.Instance.LoadScene("GameScreen_NETA", 1.0f);
            //if (btn.name == "TwitterButton")
            //{
            //    var url = "https://twitter.com/intent/tweet?"
            //        + "text=" + "今回の記録は『"
            //        + clearText.text
            //        + "』点";
            //    Application.OpenURL(url);
            //}
            //if (btn.name == "OptionButton" || btn.name == "CreditButton" || btn.name == "Option_home" || btn.name == "Credit_home")
            //{
            //    Active.SetActive(true);
            //    NotActive.SetActive(false);
            //}

            switch (ParamBridge.updateSignal)
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

        }


        public void SwitchScreen()
        {
            var sm = ParamBridge.SMode;
            FadeManager.Instance.LoadScene(sm.ToStringQuickly(), 1.0f);
            Debug.Log($"changed to {sm}");
        }

        public void SwitchView()
        {
            var curr = ParamBridge.prevVMode;
            var next = ParamBridge.VMode;
            Array.Find(views, v => v.name == next.ToStringQuickly()).SetActive(true);
            Array.Find(views, v => v.name == curr.ToStringQuickly()).SetActive(false);
        }

    }
}