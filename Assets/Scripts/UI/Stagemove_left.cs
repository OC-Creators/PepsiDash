using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface{
	
	public class Stagemove_left : MonoBehaviour
	{
		public RectTransform stage1;
		public RectTransform stage2;
		public GameObject image1;
		public GameObject image2;
		private int counter = 0;
		//private int click = 0;
		private float move = -6f;
		bool left = false;
		public static bool stage1select = true;

		public void OnClick(){
				left = true;
		}

		void Update(){
			if(stage1select){
				image1.SetActive(true);
				image2.SetActive(false);
			}

			if(left==true && stage1select){
				stage1.position += new Vector3(move,0,0);
				stage2.position += new Vector3(move,0,0);
				counter++;
				if(counter == 100){
					stage1.position += new Vector3(0,0,0);
					stage2.position += new Vector3(0,0,0);
					counter = 0;
					left = false;
					stage1select = false;
					Stagemove_right.stage2select = true;
				}
			}else{
				left = false;
			}
		}
	}
}