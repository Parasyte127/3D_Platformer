using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    public Transform myPlatform;
    public Transform myStartPoint;
    public Transform myEndPoint;

    bool orbExists;
    public GameObject orbChecker;
    public LayerMask orbLayer;

    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        myPlatform.position = myStartPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
         orbExists = Physics.CheckSphere(orbChecker.transform.position, 50.0f, orbLayer);

        if (orbExists == false)
        {
            myPlatform.position = Vector3.MoveTowards(myPlatform.position, myEndPoint.position, speed);
        }
    }
}
