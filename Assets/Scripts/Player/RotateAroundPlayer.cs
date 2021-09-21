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

        private Vector3 firstVectorOnPlaneXZ;

        private Vector3 transformVectorOnPlaneXZ;

        private Vector3 firstVectorOnPlaneYZ;

        private Vector3 transformVectorOnPlaneYZ;

        private float preAngleY;

        private float angleY;

        private float angleX;

        //private Transform preTransformX;

        private Vector3 prePositionX;

        private Quaternion preRotationX;

        [SerializeField] [Range(0f, 50f)] private float maxAngleX = 30f;

        private float minAngleX;


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
            v = CrossPlatformInputManager.GetAxis("ViewVertical");
            r = CrossPlatformInputManager.GetButton("Reset");

            if(r == false)
            {
                centerPos = center.position;
                //transform.RotateAround(centerPos, Vector3.up, 360 / 2 * Time.deltaTime * h * rotateSpeed);
                //transform.RotateAround(centerPos, Vector3.right, 360 / 2 * Time.deltaTime * v * rotateSpeed);
                transform.RotateAround(centerPos, Vector3.up, 360 / 2 * Time.deltaTime * h * rotateSpeed);// 横回転

                minAngleX = 360f - maxAngleX;
                prePositionX = this.transform.position;
                preRotationX = this.transform.rotation;
                this.transform.RotateAround(centerPos, transform.right, 360 / 2 * Time.deltaTime * v * rotateSpeed * -1);

                //Debug.Log(transform.localEulerAngles.x);

                if (this.transform.localEulerAngles.x >= maxAngleX && this.transform.localEulerAngles.x <= minAngleX)
                {
                    transform.position = prePositionX;
                    transform.rotation = preRotationX;
                }
                //transform.RotateAround(centerPos, transform.right, 360 / 2 * Time.deltaTime * v * rotateSpeed * -1);

                //followPlayer.setDistance(transform.position - center.position);
            }
            else
            {
                centerPos = center.position;
                /*
                transform.position = Vector3.Lerp(transform.position, firstPositionTransform.position, Time.deltaTime * resetRotateSpeed);
                transform.forward = Vector3.Slerp(transform.forward, firstPositionTransform.forward, Time.deltaTime * resetRotateSpeed);
                */
                firstVectorOnPlaneXZ = new Vector3(firstPositionTransform.forward.x, 0, firstPositionTransform.forward.z);
                transformVectorOnPlaneXZ = new Vector3(transform.forward.x, 0, transform.forward.z);
                angleY = Vector3.SignedAngle(transformVectorOnPlaneXZ, firstVectorOnPlaneXZ, Vector3.up);
                transform.RotateAround(centerPos, Vector3.up, angleY * Time.deltaTime * resetRotateSpeed);

                //firstVectorOnPlaneYZ = new Vector3(0, firstPositionTransform.forward.y, firstPositionTransform.forward.z);
                //transformVectorOnPlaneYZ = new Vector3(0, transform.forward.y, firstPositionTransform.forward.z);
                //angleX = Vector3.SignedAngle(transformVectorOnPlaneYZ, firstVectorOnPlaneYZ, Vector3.right);
                //transform.RotateAround(centerPos, transform.right, angleX * Time.deltaTime * resetRotateSpeed);

                preAngleY = transform.localEulerAngles.x;
                if (preAngleY > 180f) preAngleY -= 360f;
                angleX = firstPositionTransform.localEulerAngles.x - preAngleY;
                transform.RotateAround(centerPos, transform.right, angleX * Time.deltaTime * resetRotateSpeed);
            }
        }
    }
}
