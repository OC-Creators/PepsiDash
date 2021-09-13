using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

namespace General {
    
    public class GameManager : ScreenManager<GameManager>
    {
        [SerializeField] private RawImage openingRawImage;
        [SerializeField] private RawImage wonderfulResultRawImage;
        [SerializeField] private RawImage niceResultRawImage;
        [SerializeField] private RawImage badResultRawImage;

        private VideoPlayer vp;
        private RawImage rawImg;
        
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
            pb.Elapsed = 0f;
            gfc.dispatch(Signal.Forward);
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

        public void PlayOpeningMovie()
        {
            vp = openingRawImage.GetComponent<VideoPlayer>();
            rawImg = openingRawImage;
                    
            // 再生準備完了
            vp.prepareCompleted += (VideoPlayer videoPlayer) =>
            {
                Debug.Log($"Prepare Completed: Opening");
                pb.StopTheWorld = true;
                am.Stop();
                // 動画再生
                videoPlayer.Play();
            };
            // 再生開始
            vp.started += (VideoPlayer videoPlayer) =>
            {
                rawImg.enabled = true;
            };
            // 再生終了
            vp.loopPointReached += (VideoPlayer videoPlayer) =>
            {
                rawImg.enabled = false;
                gfc.SwitchView(ViewMode.InGame);
                am.Play(BGM_NAME);
                pb.StopTheWorld = false;
                Time.timeScale = 1f;
            };
            // 再生準備
            vp.Prepare();
        }

        public void PlayResultMovie(Result res)
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

            // 再生準備完了
            vp.prepareCompleted += (VideoPlayer videoPlayer) =>
            {
                Debug.Log($"Prepare Completed: {res}");
                pb.StopTheWorld = true;
                am.Play(FANFARE_NAME);
                // 動画再生
                videoPlayer.Play();
            };
            // 再生開始
            vp.started += (VideoPlayer videoPlayer) =>
            {
                rawImg.enabled = true;
            };
            // 再生終了
            vp.loopPointReached += (VideoPlayer videoPlayer) =>
            {
                gfc.SwitchView(ViewMode.Result, currActive: true);
            };
            // 再生準備
            vp.Prepare();
        }
    }

    public enum Result
    {
        Wonderful,
        Nice,
        Bad
    }
}