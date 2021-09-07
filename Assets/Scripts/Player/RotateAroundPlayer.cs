using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Player
{
    public class RotateAroundPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform center;

        private Vector3 centerPos;

        private float h;

        private float v;

        private bool r;

        //private FollowPlayer followPlayer;

        [Range(0.1f, 2f)] public float rotateSpeed = 1.0f;

        [SerializeField]
        private Transform firstPositionTransform;

        [Range(0.1f, 10f)] public float resetRotateSpeed = 5.0f;

        private Vector3 firstVectorOnPlane;

        private Vector3 transformVectorOnPlane;

        private float angle;


        // Start is called before the first frame update
        void Start()
        {
            if (center == null) center = transform.parent;

            //followPlayer = GetComponent<FollowPlayer>();
            firstPositionTransform.position = transform.position;
            firstPositionTransform.forward = transform.forward;
        }

        // Update is called once per frame
        void Update()
        {
            RotateCamera();
        }

        void RotateCamera()
        {
            h = CrossPlatformInputManager.GetAxis("ViewHorizontal");
            //v = CrossPlatformInputManager.GetAxis("ViewVertical");
            r = CrossPlatformInputManager.GetButton("Reset");

            if(r == false)
            {
                centerPos = center.position;
                transform.RotateAround(centerPos, Vector3.up, 360 / 2 * Time.deltaTime * h * rotateSpeed);
                //followPlayer.setDistance(transform.position - center.position);
            } else
            {
                /*
                transform.position = Vector3.Lerp(transform.position, firstPositionTransform.position, Time.deltaTime * resetRotateSpeed);
                transform.forward = Vector3.Slerp(transform.forward, firstPositionTransform.forward, Time.deltaTime * resetRotateSpeed);
                */
                firstVectorOnPlane = new Vector3(firstPositionTransform.forward.x, 0, firstPositionTransform.forward.z);
                transformVectorOnPlane = new Vector3(transform.forward.x, 0, transform.forward.z);
                angle = Vector3.SignedAngle(transformVectorOnPlane, firstVectorOnPlane, Vector3.up);
                transform.RotateAround(centerPos, Vector3.up, angle * Time.deltaTime * resetRotateSpeed);
            }
        }
    }
}
