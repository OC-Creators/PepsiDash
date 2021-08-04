using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class StageSelectScreenManager : MonoBehaviour
    {
        public ParamBridge bridge;
        public AudioSource audio;
        // Start is called before the first frame update
        void Start()
        {
            audio.volume = Menu_home.volume;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}