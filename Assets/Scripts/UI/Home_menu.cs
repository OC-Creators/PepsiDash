using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_menu : MonoBehaviour
{
	public GameObject Option;
	//public GameObject Start;
	public GameObject Option2;
	//public GameObject credit;

	public void OnClickStartButton(){
			Option.SetActive(true);
			//Start.SetActive(false);
			//credit.SetActive(false);
			Option2.SetActive(false);
		}
}