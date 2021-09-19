﻿using System;
using UnityEngine;

namespace General {
    [Serializable]
    public class Param
    {
        [SerializeField]
        public float bgm_volume = 1f;
        [SerializeField]
        public float se_volume = 1f;
        [SerializeField]
        public Result high_score = Result.None;
        [SerializeField]
        public int unlock = 0;
    }
}