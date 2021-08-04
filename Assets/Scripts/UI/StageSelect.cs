using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
	GameObject stage1;
	GameObject stage2;
	Button stage1_button;
	Button stage2_button;

    // Start is called before the first frame update
    void Start()
    {
		stage1_button = GameObject.Find ("Stage/Stage1Button").GetComponent<Button> ();
		stage2_button = GameObject.Find ("Stage/Stage2Button").GetComponent<Button> ();
        
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
