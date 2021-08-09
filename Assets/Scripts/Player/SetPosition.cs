using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    //初期位置
    private Vector3 startPosition;
    //目的地
    private Vector3 destination;


    //周回
    /*[SerializeField]
    private Vector3 dest1 = new Vector3(3f, 0f, 0f);
    [SerializeField]
    private Vector3 dest2 = new Vector3(0f, 0f, 3f);
    [SerializeField]
    private Vector3 dest3 = new Vector3(0f, 0f, -3f);
    [SerializeField]
    private Vector3 dest4 = new Vector3(0f, 0f, -3f);
    private Vector3[] destPos;*/

    public GameObject Root;

    private Transform[] points;


    // Start is called before the first frame update
    void Start()
    {
        //　初期位置を設定
        startPosition = transform.position;
        SetDestination(transform.position);
        //destPos = new Vector3[] { dest1, dest2, dest3, dest4 };

        if (Root != null) points = Root.GetComponent<RootSetting>().points;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(points.Length);
    }
    //　ランダムな位置の作成
    public void CreateRandomPosition()
    {
        //　ランダムなVector2の値を得る
        var randDestination = Random.insideUnitCircle * 8;
        //　現在地にランダムな位置を足して目的地とする
        SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
    }

    public void NextPosition(int i)
    {
        //SetDestination(destPos[i]);
        SetDestination(points[i].position);
    }

    //　目的地を設定する
    public void SetDestination(Vector3 position)
    {
        destination = position;
        //Debug.Log("sepo :" + destination);
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }

    public int PointsNum()
    {
        return points.Length;
    }
}
