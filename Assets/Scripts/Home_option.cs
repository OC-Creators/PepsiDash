using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_option : MonoBehaviour
{
	public GameObject Option;
	public GameObject Home;
	public void OnclickStartButton()
	{
			Option.SetActive(false);
			Home.SetActive(true);
	}
}
