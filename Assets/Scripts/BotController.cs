using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BotController : MonoBehaviour
{
    //public float moveSpeed = 5;

    //public float rotationSpeed = 1;

    public GameObject[] checkPoints;

    //public Rigidbody2D rb;

    //public Transform zoidTransform;

    //public Transform headTransform;

    public AIDestinationSetter aIDestinationSetter;

    private int nextCheckPoint;


    void Start()
    {
        nextCheckPoint = 0;
        aIDestinationSetter.target = checkPoints[nextCheckPoint].transform;
    }

    private void FixedUpdate()
    {        
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {      

        if (other.gameObject == checkPoints[nextCheckPoint]) 
        {
            if (other.gameObject == checkPoints[checkPoints.Length - 1])
                nextCheckPoint = 0;
            else
                nextCheckPoint += 1;
            aIDestinationSetter.target = checkPoints[nextCheckPoint].transform;
        }

    }
}
