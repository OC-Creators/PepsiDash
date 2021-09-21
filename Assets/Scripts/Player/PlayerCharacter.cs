using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class PlayerCharacter : MonoBehaviour
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

		[SerializeField] private Transform head;

		private bool isVoid = false;

		private bool isChange = false;

		[SerializeField] private bool isCoolTime = false;

		[SerializeField] [Range(0, 5)] private int countIntoVoid = 2;

		[SerializeField] [Range(0f, 60f)] private float voidTime = 5f;

		private float elapsedVoidTime = 0f;

		[SerializeField] [Range(0f, 50f)] private float coolTime = 15f;

		private float elapsedCoolTime = 0f;

		[SerializeField] [Range(0f, 1f)] private float transparentAlfa = 0.2f;

		[SerializeField] [Range(0f, 10f)] private float alfaSpeed = 2f;

		[SerializeField] private SkinnedMeshRenderer surface;

		[SerializeField] private SkinnedMeshRenderer joints;

		private Color surfaceColor;

		private Color jointsColor;

		[SerializeField] private Material[] m_Surface;

		[SerializeField] private Material[] m_Joints;

		[SerializeField] [Range(0f, 1f)] private float decelerateSpeedRate = 0.2f;

		[SerializeField] [Range(0f, 5f)] private float voidRunSpeedRate = 2f;



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


		public void Move(Vector3 move, Vector3 camForward, Vector3 input, bool crouch, bool modeVoid, bool jump, bool dash, float speedRate)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			//if (move.magnitude > 1f) move.Normalize(); 仕様変更
			//move = transform.InverseTransformDirection(move);
			CheckGroundStatus();

			//if (input.z > 0) m_Rigidbody.MovePosition(transform.position + camForward.normalized * Time.deltaTime * input.z * speed);
			//m_Rigidbody.MovePosition(transform.position + camForward.normalized * Time.deltaTime * input.z * speed);　仕様変更
			// 移動
			if (isVoid)
            {
				m_Rigidbody.MovePosition(transform.position + move.normalized * Time.deltaTime * speed * voidRunSpeedRate * speedRate);
			}
			else if (crouch)
            {
				m_Rigidbody.MovePosition(transform.position + move.normalized * Time.deltaTime * speed * decelerateSpeedRate * speedRate);

			}
			else
            {
				m_Rigidbody.MovePosition(transform.position + move.normalized * Time.deltaTime * speed);
			}

			if (move.magnitude > 0.1f) transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * speed * speedRate);// 方向転換

			/* ジャンプ廃止
			if (m_IsGrounded && jump)
			{
				m_Rigidbody.AddForce(Vector3.up * jumpForce);
				m_IsGrounded = false;
			}
			*/

			// 仕様変更につき
			//move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			//m_TurnAmount = Mathf.Atan2(move.x, move.z);
			//m_ForwardAmount = move.z;

			//ApplyExtraTurnRotation(input);

			//虚空システム
			IntoVoid(modeVoid);

			//ScaleCapsuleForJumping(jump);ジャンプ廃止
			ScaleCapsuleForCrouching(crouch);


			UpdateAnimator(move, crouch, jump, speedRate);

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


		void ScaleCapsuleForCrouching(bool crouch)
		{
			if (m_IsGrounded && crouch)
			{
				if (m_Crouching) return;
				m_Capsule.height = m_Capsule.height / 2f;
				m_Capsule.center = m_Capsule.center / 2f;
				m_Crouching = true;
			}
			else
			{
				Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
				float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
				if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					m_Crouching = true;
					return;
				}
				m_Capsule.height = m_CapsuleHeight;
				m_Capsule.center = m_CapsuleCenter;
				m_Crouching = false;
			}
		}


		void UpdateAnimator(Vector3 move, bool crouch, bool jump, float speedRate)
		{
			if (m_IsGrounded)
			{
				if (isVoid)
                {
					m_Animator.SetBool("Ground", false);
					m_Animator.SetBool("Crouching", false);
					m_Animator.SetBool("Void", true);
				}
				else if (crouch)
				{
					m_Animator.SetBool("Ground", false);
					m_Animator.SetBool("Crouching", true);
					m_Animator.SetBool("Void", false);
				}
				else
				{
					m_Animator.SetBool("Ground", true);
					m_Animator.SetBool("Crouching", false);
					m_Animator.SetBool("Void", false);
				}
			}

			m_Animator.SetFloat("Speed", speedRate);


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

		void IntoVoid(bool isButton)
		{
			// 入力受付
			if (!isVoid && isButton && countIntoVoid > 0 && !isCoolTime && !isChange)
			{
				isChange = true;
				surface.material = m_Surface[1];
				joints.material = m_Joints[1];
				// エフェクトをオンにするかも
			}

			// 虚空中の動作 それ以外も(クールタイム)
			if (isVoid)
			{
				elapsedVoidTime += Time.deltaTime;
				//Debug.Log("Player in the void.");

				if (elapsedVoidTime > voidTime)
				{
					isCoolTime = true;
					isChange = true;
					countIntoVoid--;
					elapsedVoidTime = 0f;
					elapsedCoolTime = 0f;
					//Debug.Log("Leave from void.");
					// プレイヤーのテクスチャーを戻す
					// エフェクトをオフにする
				}
			}
			else
			{
				if (isCoolTime)
				{
					elapsedCoolTime += Time.deltaTime;
					if (elapsedCoolTime > coolTime) isCoolTime = false;
				}
			}

			// 変化中の動作
			if (isChange)
			{
				if (isVoid)
				{
					//透明度を上げる(元に戻る)
					surfaceColor = surface.material.color;
					jointsColor = joints.material.color;
					surfaceColor.a += Time.deltaTime * alfaSpeed;
					jointsColor.a += Time.deltaTime * alfaSpeed;

					if (surfaceColor.a >= 1f) // 透明度が一定値より上になったら
					{
						surfaceColor.a = 1f;
						jointsColor.a = 1f;
						surface.material.color = surfaceColor;
						joints.material.color = jointsColor;
						surface.material = m_Surface[0];
						joints.material = m_Joints[0];
						isVoid = false;
						isChange = false;
					}
					surface.material.color = surfaceColor;
					joints.material.color = jointsColor;
				}
				else
				{
					//透明度を下げる(虚空に入る)
					surfaceColor = surface.material.color;
					jointsColor = joints.material.color;
					surfaceColor.a -= Time.deltaTime * alfaSpeed;
					jointsColor.a -= Time.deltaTime * alfaSpeed;

					if (surfaceColor.a < transparentAlfa) // 透明度が一定値より下になったら
					{
						surfaceColor.a = transparentAlfa;
						jointsColor.a = transparentAlfa;
						isVoid = true;
						isChange = false;
					}
					surface.material.color = surfaceColor;
					joints.material.color = jointsColor;
				}
			}
		}

		public bool getIsVoid()
		{
			return isVoid;
		}

		public Transform getHead()
        {
			return head;
        }
	}
}
