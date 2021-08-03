using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowmove : MonoBehaviour
{
	public RectTransform arrow;
	private int counter = 0;
	private float move = 1f;

	void Update()
	{
		arrow.position += new Vector3(0, move, 0);
		counter++;
		if (counter == 100)
		{
			counter = 0;
			move *= -1;
		}
	}
}