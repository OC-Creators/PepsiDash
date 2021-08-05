using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinAniTest : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            animator.SetBool("SHTL", true);
        }else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S");
            animator.SetBool("SHTR", true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q");
            animator.SetBool("SQTL", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            animator.SetBool("SQTR", true);
        }
        else
        {
            /animator.SetBool("SHTL", false);
            animator.SetBool("SHTR", false);
            animator.SetBool("SQTL", false);
            animator.SetBool("SQTR", false);
        }*/
    }
}
