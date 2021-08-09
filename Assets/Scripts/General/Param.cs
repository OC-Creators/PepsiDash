using System;
using UnityEngine;

namespace General {
    [Serializable]
    public class Param
    {
        [SerializeField]
        public float bgm_volume { get; set; } = 1f;
        [SerializeField]
        public float se_volume { get; set; } = 1f;
        [SerializeField]
        public int score { get; set; } = 0;
        [SerializeField]
        public int unlock { get; set; } = 0;
    }
}