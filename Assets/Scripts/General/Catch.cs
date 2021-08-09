using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class Catch : MonoBehaviour
    {

        //public GameManager gm;

        // Start is called before the first frame update
        void Start()
        {
            //if (gm == null) GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.Catched = true;
                //Debug.Log("捕まえましてん");
            }
        }
    }
}
