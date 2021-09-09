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

        public void PlayMovie(Movie movie)
        {
            VideoPlayer vp = null;
            RawImage rawImg = null;
            switch (movie)
            {
                case Movie.Opening:
                    vp = openingRawImage.GetComponent<VideoPlayer>();
                    rawImg = openingRawImage;
                    break;
                case Movie.WonderfulResult:
                    vp = wonderfulResultRawImage.GetComponent<VideoPlayer>();
                    rawImg = wonderfulResultRawImage;
                    break;
                case Movie.NiceResult:
                    vp = niceResultRawImage.GetComponent<VideoPlayer>();
                    rawImg = niceResultRawImage;
                    break;
                case Movie.BadResult:
                    vp = badResultRawImage.GetComponent<VideoPlayer>();
                    rawImg = badResultRawImage;
                    break;
            }

            vp.prepareCompleted += (VideoPlayer videoPlayer) =>
            {
                Debug.Log($"Prepare Completed: {rawImg}");

                videoPlayer.started += (VideoPlayer videoPlayer2) =>
                {
                    rawImg.enabled = true;
                };
                videoPlayer.loopPointReached += (VideoPlayer videoPlayer2) =>
                {
                    videoPlayer2.Stop();
                    rawImg.enabled = false;
                };
                videoPlayer.Play();
            };
            vp.Prepare();
        }
    }

    public enum Movie
    {
        Opening,
        WonderfulResult,
        NiceResult,
        BadResult
    }
}