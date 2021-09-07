using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface {

	public class StageSelect : MonoBehaviour
	{
		GameObject stage1;
		GameObject stage2;
		UnityEngine.UI.Button stage1_button;
		UnityEngine.UI.Button stage2_button;

		// Start is called before the first frame update
		void Start()
		{
			stage1_button = GameObject.Find ("Canvas/Stage1Button").GetComponent<UnityEngine.UI.Button> ();
			stage2_button = GameObject.Find ("Canvas/Stage2Button").GetComponent<UnityEngine.UI.Button> ();
		}

		void Update(){
			// Update is called once per frame
			//Stagemove_left.stage1select;
			if(Stagemove_left.stage1select){
				stage1_button.enabled = true;
				stage2_button.enabled = false;
			}else{
				stage1_button.enabled = false;
				stage2_button.enabled = true;
			}	
		}
	}
}