using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
	public GameObject sphere;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			sphere.SetActive(true);
		}
		else if(Input.GetMouseButtonDown(1))
		{
			sphere.SetActive(false);
		}
	}
}