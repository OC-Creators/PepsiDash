using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class StairUpMove : MonoBehaviour
    {

        [SerializeField] private Transform stairStart;

        [SerializeField] private Transform stairEnd;

        private Vector3 forward;

        [SerializeField] [Range(0f, 30f)]private float speed = 1f;

        private Vector3 stairEndPositionXZ;


        // Start is called before the first frame update
        void Start()
        {
            forward = stairEnd.position - stairStart.position;
            forward.Normalize();

            stairEndPositionXZ = Vector3.Scale(stairEnd.position, new Vector3(1, 0, 1));
        }

        // Update is called once per frame
        void Update()
        {
            if ((Vector3.Scale(transform.position, new Vector3(1, 0, 1)) - stairEndPositionXZ).magnitude > 0.1f)
            {
                Move();
            }
        }

        void Move()
        {
            transform.position += forward * Time.deltaTime * speed;
        }
    }
}
