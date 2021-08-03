using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRamdom : MonoBehaviour
{
    private CharacterController enemyController;
    private Animator animator;
    //　目的地
    private Vector3 destination;
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    // 到着フラグ
    private bool arrived;
    // SetPositionスクリプトを持ってくる。
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    // 周回配列添字
    private int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        //setPosition.NextPosition(num);
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        destination = setPosition.GetDestination();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived)
        {

            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (destination - transform.position).normalized;
                transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * walkSpeed;
                // Debug.Log(destination);
                // Debug.Log(transform.position);
            }
            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);

            // 到着判定
            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                arrived = true;
                animator.SetFloat("Speed", 0.0f);
            }
        }
        else
        {
            // 到着
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                //num++;
                setPosition.CreateRandomPosition();
                destination = setPosition.GetDestination();
                arrived = false;
                elapsedTime = 0f;
            }
            //Debug.Log(elapsedTime);

        }
    }
}
