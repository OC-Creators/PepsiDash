using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MoveEnemy : MonoBehaviour
    {
        private CharacterController enemyController;
        private Animator animator;
        //　目的地
        private Vector3 destination;
        [SerializeField]
        private float walkSpeed = 1.0f;
        [SerializeField]
        private float runSpeed = 2.0f;
        //　速度
        private Vector3 velocity;
        //　移動方向
        private Vector3 direction;
        // 到着フラグ
        public bool arrived;
        // SetPositionスクリプトを持ってくる。
        private SetPosition setPosition;
        //　待ち時間
        [SerializeField]
        private float patrolWaitTime = 5f;

        [SerializeField]
        private float cautionWaitTime = 60f;

        [SerializeField]
        private float chaseWaitTime = 60f;

        [SerializeField]
        private float warningWaitTime = 60f;
        //　経過時間
        private float elapsedTime;
        // 周回配列添字
        private int num = 0;


        private string state;

        private string prevState;

        private Vector3 playerPos;

        //private Transform firstPlayerTransform;

        private Vector3 firstPlayerPos;

        private Quaternion firstPlayerRotate;

        //private Finder finder;

        [Range(0f, 10f)]
        [SerializeField]
        private float rotateSpeed = 5f;


        // Start is called before the first frame update
        void Start()
        {
            setPosition = GetComponent<SetPosition>();
            //finder = GetComponent<Finder>();
            //setPosition.CreateRandomPosition();
            setPosition.NextPosition(num);
            enemyController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            destination = setPosition.GetDestination();
            velocity = Vector3.zero;
            //firstPlayerTransform = this.transform;
            firstPlayerPos = this.transform.position;
            firstPlayerRotate = this.transform.rotation;
            arrived = false;
            elapsedTime = 0f;
            state = "patrol";
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Animation();
        }

        void Move()
        {
            switch (state)
            {
                case "patrol":
                    if (!arrived)
                    {
                        if (enemyController.isGrounded)
                        {
                            velocity = Vector3.zero;
                            //animator.SetFloat("Speed", 2.0f);
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
                            //animator.SetFloat("Speed", 0.0f);
                        }
                    }
                    else
                    {
                        // 到着
                        elapsedTime += Time.deltaTime;


                        if (elapsedTime > patrolWaitTime)
                        {
                            if (num < setPosition.PointsNum() - 1)
                            {
                                num++;
                                setPosition.NextPosition(num);
                                destination = setPosition.GetDestination();
                                //Debug.Log("Point : " + num);
                                arrived = false;
                                elapsedTime = 0f;
                            }
                            else if (num == setPosition.PointsNum() - 1)
                            {
                                num = 0;
                                //Debug.Log("Point : owari");
                                setPosition.NextPosition(num);
                                destination = setPosition.GetDestination();
                                arrived = false;
                                elapsedTime = 0f;
                                //Debug.Log("syokika");
                            }
                        }
                        /*
                        //　待ち時間を越えたら次の目的地を設定
                        if (elapsedTime > waitTime && num < 3)
                        {
                            num++;
                            Debug.Log(num);
                            setPosition.NextPosition(num);
                            destination = setPosition.GetDestination();
                            arrived = false;
                            elapsedTime = 0f;
                        }
                        if (elapsedTime > waitTime && num == 3)
                        {
                            Debug.Log("owari"+num);
                            setPosition.NextPosition(num);
                            destination = setPosition.GetDestination();
                            arrived = false;
                            elapsedTime = 0f;
                            num = -1;
                            Debug.Log("syokika"+num);
                        }
                        */
                        //Debug.Log(elapsedTime);

                    }
                    break;
                case "caution":
                    //transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
                    // このあと分岐させたい、時間とかの調整によってchangeState("patrol")もしくはchangeState("chase")に変化させるなど
                    break;
                case "chase":
                    setPosition.SetDestination(playerPos);
                    if (enemyController.isGrounded)
                    {
                        velocity = Vector3.zero;
                        //animator.SetFloat("Speed", 2.0f); 
                        destination = setPosition.GetDestination();
                        direction = (destination - transform.position).normalized;
                        transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                        velocity = direction * runSpeed;
                    }
                    velocity.y += Physics.gravity.y * Time.deltaTime;
                    enemyController.Move(velocity * Time.deltaTime);
                    break;
                case "warning":
                    if (!arrived)
                    {
                        if (enemyController.isGrounded)
                        {
                            velocity = Vector3.zero;
                            direction = (destination - transform.position).normalized;
                            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                            velocity = direction * walkSpeed;
                        }
                        velocity.y += Physics.gravity.y * Time.deltaTime;
                        enemyController.Move(velocity * Time.deltaTime);

                        // 到着判定
                        if (Vector3.Distance(transform.position, destination) < 0.2f)
                        {
                            arrived = true;
                        }
                    }
                    else
                    {
                        // 到着
                        elapsedTime += Time.deltaTime;

                        Quaternion rot = Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, Vector3.up);
                        this.transform.rotation = rot * transform.rotation;

                        if (elapsedTime > patrolWaitTime)
                        {
                            resetEnemyPosition();
                            changeState("patrol");
                        }
                    }
                    break;
            }
        }


        void Animation()
        {
            if (prevState != state)
            {
                switch (state)
                {
                    case "patrol":
                        animator.SetBool("patrol", true);
                        animator.SetBool("caution", false);
                        animator.SetBool("chase", false);
                        animator.SetBool("warning", false);
                        break;
                    case "caution":
                        animator.SetBool("patrol", false);
                        animator.SetBool("caution", true);
                        animator.SetBool("chase", false);
                        animator.SetBool("warning", false);
                        break;
                    case "chase":
                        animator.SetBool("patrol", false);
                        animator.SetBool("caution", false);
                        animator.SetBool("chase", true);
                        animator.SetBool("warning", false);
                        break;
                    case "warning":
                        animator.SetBool("patrol", false);
                        animator.SetBool("caution", false);
                        animator.SetBool("chase", false);
                        animator.SetBool("warning", true);
                        break;
                }
                prevState = state;
            }
        }

        public void changeState(string newState)
        {
            switch (newState)
            {
                case "patrol":
                    arrived = false;
                    elapsedTime = 0f;
                    state = newState;
                    //num = 0;
                    //resetEnemyPosition(); // 初期位置、方向に飛ばす関数
                    setPosition.NextPosition(num);
                    destination = setPosition.GetDestination();
                    break;
                case "caution":
                    state = newState;
                    elapsedTime = 10f;
                    break;
                case "chase":
                    state = newState;
                    arrived = false;
                    elapsedTime = 0f;
                    break;
                case "warning":
                    state = newState;
                    arrived = false;
                    elapsedTime = 0f;
                    destination = setPosition.GetDestination();
                    break;
            }
        }

        public string getState()
        {
            return state;
        }

        public void changeElapsedTime(bool isPlayer)
        {
            if (isPlayer)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                elapsedTime -= Time.deltaTime;
            }
        }

        public float getElapsedTime()
        {
            return elapsedTime;
        }

        public void setPlayerPos(GameObject player)
        {
            if (player != null)
            {
                playerPos = player.transform.position;
                //Debug.Log("SetPlayerPos!!");
            }


        }

        void resetEnemyPosition()
        {
            num = 0;

            enemyController.enabled = false;

            transform.position = firstPlayerPos;
            transform.rotation = firstPlayerRotate;

            enemyController.enabled = true;
        }
    }
}