using UnityEngine;
using System.Collections;

public class RinController3 : MonoBehaviour
{

	private Animator animator;
	private CharacterController characterController;
	//　速度
	private Vector3 velocity;
	//　ジャンプ力
	[SerializeField]
	private float jumpPower = 5f;

	void Start()
	{
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
		velocity = Vector3.zero;
	}

	void Update()
	{
		if (characterController.isGrounded)
		{
			velocity = Vector3.zero;
			//animator.ResetTrigger("Jump");
		}
		var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		//　方向キーが多少押されている
		if (input.magnitude > 0f)
		{
			animator.SetFloat("Speed", input.magnitude);
			transform.LookAt(transform.position + input);
			velocity.x = input.normalized.x * 2;
			velocity.z = input.normalized.z * 2;
			animator.SetBool("Run", true);
			//　キーの押しが小さすぎる場合は移動しない
		}
		else
		{
			animator.SetFloat("Speed", 0f);
			animator.SetBool("Run", false);
		}
		//　ジャンプキー（デフォルトではSpace）を押したらY軸方向の速度にジャンプ力を足す
		/*
		if (Input.GetButtonDown("Jump")
			&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
			&& !animator.IsInTransition(0)
		)
		*/
		if (Input.GetButtonDown("Jump"))
		{
			animator.SetBool("Jump", true);
			//animator.SetTrigger("Jump");
			if (characterController.isGrounded) velocity.y += jumpPower;
		} else
        {
			animator.SetBool("Jump", false);
        }

		velocity.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);
	}
}