using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Player
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        private float speedRate;

        [SerializeField] [Range(0f, 1f)] private float walkSpeedRate = 1f;


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<PlayerCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = CrossPlatformInputManager.GetButton("Crouch");
            bool modeVoid = CrossPlatformInputManager.GetButton("Void");
            bool dash = CrossPlatformInputManager.GetButton("Dash");

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                //m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                //m_Move = v * m_CamForward + h * m_Cam.right;

                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1));
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            m_Move.Normalize();

            // PC版
            /* 初版
            if (m_Move.magnitude < 0.1f)
            {
                speedRate -= Time.deltaTime;
                if (speedRate < 0f) speedRate = 0f;
            }
            else
            {
                if (dash)
                {
                    speedRate += Time.deltaTime * 2f;
                    if (speedRate > 1f) speedRate = 1f;
                } else
                {
                    if (speedRate < walkSpeedRate)
                    {
                        speedRate += Time.deltaTime;
                        if (speedRate > walkSpeedRate) speedRate = walkSpeedRate;
                    } else if (speedRate > walkSpeedRate)
                    {
                        speedRate -= Time.deltaTime;
                        if (speedRate < walkSpeedRate) speedRate = walkSpeedRate;
                    }
                }
            }
            */
            if (m_Move.magnitude < 0.1f)
            {
                speedRate -= Time.deltaTime * 6f;
                if (speedRate < 0f) speedRate = 0f;
            } else
            {
                speedRate += Time.deltaTime * 4f;
                if (speedRate > walkSpeedRate) speedRate = walkSpeedRate;
            }

            // スマホ版
            // speedRate = Input.magnitude;

            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_CamForward, new Vector3(h, 0, v), crouch, modeVoid, m_Jump, dash, speedRate);
            m_Jump = false;
        }
    }
}
