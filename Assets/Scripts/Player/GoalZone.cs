using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GoalZone : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) Debug.Log("Goal!!");
        }

    }
}
