using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface{
	
	public class Stagemove_right : MonoBehaviour
	{
		public RectTransform stage1;
		public RectTransform stage2;
		public GameObject image1;
		public GameObject image2;
		private int counter = 0;
		private float move = 6f;
		bool right = false;
		public static bool stage2select = false;


		public void OnClick(){
			right = true;
		}

		void Update(){
			if(stage2select){
				image1.SetActive(false);
				image2.SetActive(true);
			}
			if(right==true && stage2select){
				stage1.position += new Vector3(move,0,0);
				stage2.position += new Vector3(move,0,0);
				counter++;

				if(counter == 100){
					stage1.position += new Vector3(0,0,0);
					stage2.position += new Vector3(0,0,0);
					//Debug.Log(stage1.position.x);
					//Debug.Log(stage2.position.x);
					counter = 0;
					right = false;
					stage2select = false;
					Stagemove_left.stage1select = true;
				}
			}else{
				right=false;
			}
		}
	}
}