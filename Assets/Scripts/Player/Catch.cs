using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Catch : MonoBehaviour
    {
        void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                General.ParamBridge.Instance.Catched = true;
                Debug.Log("catched!");
            } 
                
        }
    }
}
