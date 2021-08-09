using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Catch : MonoBehaviour
    {

        public GameManager gm;

        // Start is called before the first frame update
        void Start()
        {
            if (gm == null) GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player")) gm.Instance.Catched = true;
        }
    }
}
