using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagemove_left : MonoBehaviour
{
	public RectTransform stage1;
	public RectTransform stage2;
	private int counter = 0;
	//private int click = 0;
	private float move = -6f;
	bool left = false;

	public void OnClick(){
			left = true;
	}

	void Update(){
		
		if(left==true&& stage2.position.x > 459){
			stage1.position += new Vector3(move,0,0);
			stage2.position += new Vector3(move,0,0);
			counter++;
			if(counter == 100){
				stage1.position += new Vector3(0,0,0);
				stage2.position += new Vector3(0,0,0);
				counter = 0;
				left = false;
			}
		}else{
			left = false;
		}
	}
}