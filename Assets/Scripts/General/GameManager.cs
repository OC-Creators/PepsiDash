using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;
using System.Collections;

namespace General {
    
    public class GameManager : ScreenManager<GameManager>
    {
        [SerializeField] private RawImage openingRawImage;
        [SerializeField] private RawImage wonderfulResultRawImage;
        [SerializeField] private RawImage niceResultRawImage;
        [SerializeField] private RawImage badResultRawImage;

        private VideoPlayer vp;
        private RawImage rawImg;

        //private IEnumerator op;
        //private IEnumerator res;

        private const string BGM_NAME = "penguin";
        private const string FANFARE_NAME = "wonderland";


        protected override void Start()
        {
            base.Start();

            if (gfc.VMode != ViewMode.GameEntry)
            {
                gfc.VMode = ViewMode.GameEntry;
            }
            if (gfc.SMode != ScreenMode.Game)
            {
                gfc.SMode = ScreenMode.Game;
            }

            gfc.Views = views;
            gfc.dispatch(Signal.Forward);

            InitGame();
        }

        protected override void Update()
        {
            if (pb.StopTheWorld)
            {
                Time.timeScale = 0f;
            }

            if (!pb.IsOver)
            {
                pb.Elapsed += Time.deltaTime;
            }
        }

        public void InitGame()
        {
            pb.Elapsed = 0f;
        }

        public IEnumerator PlayOpeningMovie()
        {
            vp = openingRawImage.GetComponent<VideoPlayer>();
            rawImg = openingRawImage;
                    
            // 再生準備
            vp.Prepare();
            
            while (!vp.isPrepared)
            {
                Debug.Log("Loading Opening Movie...");
                yield return null;
            }
            // 再生準備完了
            Debug.Log("Loading Opening Movie... Completed!");
            am.Stop();
            vp.started += _ => rawImg.enabled = true;
            vp.loopPointReached += _ => rawImg.enabled = false;
            vp.Play();

            while (vp.isPlaying)
            {
                yield return null;
            }
            // 再生終了
            Debug.Log("Opening Movie ended");
            am.Play(BGM_NAME);
            gfc.dispatch(Signal.Forward);
        }

        public IEnumerator PlayResultMovie(Result result)
        {
            //vp = null;
            //rawImg = null;
            //switch (res)
            //{
            //    case Result.Wonderful:
            //        vp = wonderfulResultRawImage.GetComponent<VideoPlayer>();
            //        rawImg = wonderfulResultRawImage;
            //        break;
            //    case Result.Nice:
            //        vp = niceResultRawImage.GetComponent<VideoPlayer>();
            //        rawImg = niceResultRawImage;
            //        break;
            //    case Result.Bad:
            //        vp = badResultRawImage.GetComponent<VideoPlayer>();
            //        rawImg = badResultRawImage;
            //        break;
            //}

            vp = openingRawImage.GetComponent<VideoPlayer>();
            rawImg = openingRawImage;

            // 再生準備
            vp.Prepare();

            while (!vp.isPrepared)
            {
                Debug.Log($"Loading Result Movie: {result}...");
                yield return null;
            }
            // 再生準備完了
            Debug.Log($"Loading Result Movie: {result}... Completed!");
            am.Play(FANFARE_NAME);
            vp.started += _ => rawImg.enabled = true;
            vp.loopPointReached += _ => rawImg.enabled = false;
            vp.Play();

            while (vp.isPlaying)
            {
                yield return null;
            }
            // 再生終了
            Debug.Log("Result Movie ended");
            gfc.dispatch(Signal.Forward);
        }
    }

}