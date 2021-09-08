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

        void Start()
        {
            gfc = GameFlowController.Instance;
            Debug.Assert(vp != null);
            Debug.Assert(rawImg != null);
            Debug.Assert(gfc.VMode == ViewMode.GameEntry);

            vp.prepareCompleted += (VideoPlayer videoPlayer) =>
            {
                rawImg.transform.width = vp.texture.width;
                rawImg.transform.height = vp.texture.height;

                vp.started += (VideoPlayer videoPlayer) =>
                {
                    rawImg.enabled = true;
                };
                vp.loopPointReached += (VideoPlayer videoPlayer) =>
                {
                    vp.Stop();
                    rawImg.enabled = false;
                };
                vp.Play();
            };
            vp.Prepare();
        }

        void Update()
        {

        }

    }
}
