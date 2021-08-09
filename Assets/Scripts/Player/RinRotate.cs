using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinRotate : MonoBehaviour
{

    //public Transform rinFrom = null;
    //public Transform rinTo = null;
    private Quaternion rotation;
    //public float rotationSpeed = 10.0f;
    public Transform head = null;
    public GameObject Search_Caution;
    public GameObject Search_Find;
    public float hosei = 1.0f;
 


    // Start is called before the first frame update
    void Start()
    {
        //rinFrom = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Search_Caution.transform.LookAt(new Vector3(head.transform.forward.x * hosei, 0, head.transform.forward.z * hosei));
        //Search_Find.transform.LookAt(new Vector3(head.transform.forward.x * hosei, 0, head.transform.forward.z * hosei));

        Search_Caution.transform.rotation = Quaternion.LookRotation(new Vector3(head.transform.forward.x * hosei, 0, head.transform.forward.z * hosei), Vector3.up);
        Search_Find.transform.rotation = Quaternion.LookRotation(new Vector3(head.transform.forward.x * hosei, 0, head.transform.forward.z * hosei), Vector3.up);

    }
}
