using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit_home : MonoBehaviour
{
	public GameObject Credit;
	public GameObject Home;

	public void OnclickStartButton()
	{
		Credit.SetActive(false);
		Home.SetActive(true);
	}
}

