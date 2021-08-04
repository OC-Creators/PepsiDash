using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RInCon : MonoBehaviour
{
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("A");
			anim.SetBool("Walk", true);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			Debug.Log("S");
			anim.SetBool("Run", true);
		}
		else
		{
			anim.SetBool("Run", false);
			anim.SetBool("Walk", false);
		}
	}
}
