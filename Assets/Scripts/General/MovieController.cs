using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace General
{
    public class MovieController : MonoBehaviour
    {
        private GameFlowController gfc;
        [SerializeField] private VideoPlayer vp;
        [SerializeField] private RawImage rawImg;

        void Update()
        {

        }

        public void Play()
        {
            gfc = GameFlowController.Instance;
            Debug.Assert(vp != null);
            Debug.Assert(rawImg != null);

            vp.prepareCompleted += (VideoPlayer videoPlayer) =>
            {
                Debug.Log($"Prepare Completed: {rawImg}");
                vp.started += (VideoPlayer videoPlayer2) =>
                {
                    rawImg.enabled = true;
                };
                vp.loopPointReached += (VideoPlayer videoPlayer2) =>
                {
                    vp.Stop();
                    rawImg.enabled = false;
                };
                vp.Play();
            };
            vp.Prepare();
        }

    }
}
