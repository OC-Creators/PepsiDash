﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager_continue : MonoBehaviour
{
    // Start is called before the first frame update
	//public Option_home script;
	//public float VALUE;
	public AudioSource Audio;
    void Start()
    {
		//VALUE = Option_home.onryou;
		Audio.volume = Option_home.onryou;
    }

    // Update is called once per frame
    void Update()
    {
		
		//Debug.Log("音量"+VALUE);
        
    }
}