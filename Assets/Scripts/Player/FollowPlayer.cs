using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        //private Vector3 distance;

        // Start is called before the first frame update
        void Start()
        {
            //if(target != null) distance = transform.position - target.position;
        }

        void LateUpdate()
        {
            transform.position = target.position;
        }

    }
}
