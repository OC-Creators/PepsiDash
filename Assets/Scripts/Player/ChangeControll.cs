using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            Debug.Log("pause");
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
