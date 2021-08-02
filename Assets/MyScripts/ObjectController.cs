using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 0.0f;
    [SerializeField]
    private float m_turnSpeed = 0.0f;
    [SerializeField]
    private float m_jumpForce = 0.0f;

    private Rigidbody m_rigidbody = null;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ControlObject();
    }

    private void ControlObject()
    {
        Vector3 moveDir = Vector3.zero;

        Vector3 forwardDir = Vector3.forward;
        Vector3 rightDir = Vector3.right;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += forwardDir;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir -= forwardDir;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += rightDir;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir -= rightDir;
        }

        if (moveDir.sqrMagnitude > Mathf.Epsilon)
        {
            moveDir = moveDir.normalized;
            Turn(moveDir);
            Move(moveDir);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move(Vector3 i_forward)
    {
        Vector3 delta = i_forward * m_moveSpeed * Time.deltaTime;
        Vector3 targetPos = transform.position + delta;
        m_rigidbody.MovePosition(targetPos);
    }

    private void Turn(Vector3 i_forward)
    {
        Quaternion toRot = Quaternion.LookRotation(i_forward);
        Quaternion fromRot = transform.rotation;

        float delta = m_turnSpeed * Time.deltaTime;
        Quaternion targetRot = Quaternion.RotateTowards(fromRot, toRot, delta);

        m_rigidbody.MoveRotation(targetRot);
    }

    private void Jump()
    {
        // えっ、このままじゃ空中でもジャンプできちゃうって！？
        // 仕様だよ！

        m_rigidbody.velocity = Vector3.zero;

        Vector3 jumpVec = Vector3.up * m_jumpForce;
        m_rigidbody.AddForce(jumpVec, ForceMode.VelocityChange);
    }

} // class ObjectController