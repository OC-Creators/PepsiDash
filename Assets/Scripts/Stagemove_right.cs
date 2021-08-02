using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagemove_right : MonoBehaviour
{
	public RectTransform stage1;
	public RectTransform stage2;
	private int counter = 0;
	private float move = 6f;
	bool right = false;


	public void OnClick(){
		right = true;
	}

	void Update(){

		if(right==true && stage1.position.x < 459){
			stage1.position += new Vector3(move,0,0);
			stage2.position += new Vector3(move,0,0);
			counter++;

			if(counter == 100){
				stage1.position += new Vector3(0,0,0);
				stage2.position += new Vector3(0,0,0);
				//Debug.Log(stage1.position.x);
				Debug.Log(stage2.position.x);
				counter = 0;
				right = false;
			}
		}else{
			right=false;
		}
	}
}