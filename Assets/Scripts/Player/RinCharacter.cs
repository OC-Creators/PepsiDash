using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class RinCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
		[Range(0f, 30f)] [SerializeField] float speed = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;
		bool m_Jumping;
		[Range(0f, 600f)] public float jumpForce = 300.0f;

		public Transform center;

		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;

			m_IsGrounded = true;

			if (center == null) center = this.transform;
		}


		public void Move(Vector3 move, Vector3 camForward, Vector3 input, bool crouch, bool jump)
		{
			
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize(); 
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();

			//if (input.z > 0) m_Rigidbody.MovePosition(transform.position + camForward.normalized * Time.deltaTime * input.z * speed);
			m_Rigidbody.MovePosition(transform.position + camForward.normalized * Time.deltaTime * input.z * speed);


			if (m_IsGrounded && jump)
            {
				m_Rigidbody.AddForce(Vector3.up * jumpForce);
				m_IsGrounded = false;
			}

			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation(input);

			ScaleCapsuleForJumping(jump);

			UpdateAnimator(input, crouch, jump);

		}


		void ScaleCapsuleForJumping(bool jump)
		{
			if (!m_IsGrounded)
			{
				if (m_Jumping) return;
				m_Capsule.height = m_Capsule.height * 0.9f;
				//m_Capsule.center += m_Capsule.center / 2f;
				m_Capsule.center = center.position;
				m_Jumping = true;
			}
			else
			{
				m_Capsule.height = m_CapsuleHeight;
				m_Capsule.center = m_CapsuleCenter;
				m_Jumping = false;
			}
		}


		void UpdateAnimator(Vector3 input, bool crouch, bool jump)
		{
			if (m_IsGrounded)
            {
                if (jump)
                {
					m_Animator.SetBool("Jump", true);
					m_Animator.SetBool("Run", false);
				} else if (Mathf.Abs(input.z) > 0)
                {
					m_Animator.SetBool("Jump", false);
					m_Animator.SetBool("Run", true);
				} else
                {
					m_Animator.SetBool("Jump", false);
					m_Animator.SetBool("Run", false);
				}
			} else
            {
				m_Animator.SetBool("Jump", true);
				m_Animator.SetBool("Run", false);
            }
		}


		void ApplyExtraTurnRotation(Vector3 input)
		{
			if (input.z < 0) return;
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
			//transform.Rotate(0, m_TurnAmount * Time.deltaTime * 180, 0);
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				//m_Animator.applyRootMotion = true;
			}
			else
			{
				//m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				//m_Animator.applyRootMotion = false;
			}
		}
	}
}
