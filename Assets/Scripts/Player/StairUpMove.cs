using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class StairUpMove : MonoBehaviour
    {

        [SerializeField] private Transform stairStart;

        [SerializeField] private Transform stairEnd;

        [SerializeField] private Transform straightEnd;

        private Vector3 upForward;

        private Vector3 straightForward;

        [SerializeField] [Range(0f, 30f)]private float speed = 1f;

        private Vector3 stairEndPositionXZ;

        private Vector3 straightEndPositionXZ;

        private int flagNum = 0;


        // Start is called before the first frame update
        void Start()
        {
            upForward = stairEnd.position - stairStart.position;
            upForward.Normalize();

            straightForward = straightEnd.position - stairEnd.position;
            straightForward.Normalize();

            stairEndPositionXZ = Vector3.Scale(stairEnd.position, new Vector3(1, 0, 1));

            straightEndPositionXZ = Vector3.Scale(straightEnd.position, new Vector3(1, 0, 1));
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            FlagCheck();
        }

        void Move()
        {
            if (flagNum == 0)
            {
                UpMove();
            } else if (flagNum == 1)
            {
                StraightMove();
            }
        }

        void UpMove()
        {
            transform.position += upForward * Time.deltaTime * speed;
        }

        void StraightMove()
        {
            transform.position += straightForward * Time.deltaTime * speed;
        }

        void FlagCheck() 
        {
            if ((Vector3.Scale(transform.position, new Vector3(1, 0, 1)) - stairEndPositionXZ).magnitude < 0.1f && flagNum == 0)
            {
                flagNum++;
            }
            else if ((Vector3.Scale(transform.position, new Vector3(1, 0, 1)) - straightEndPositionXZ).magnitude < 0.1f && flagNum == 1)
            {
                flagNum++;
            }
        }

    }
}
