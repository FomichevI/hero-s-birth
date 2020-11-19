using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriumController : Boost
{   
    private Transform finishTransform;

    public float moveSpeed;

    public float maxRadius;

    private void Start()
    {
        lifeTime = 10;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishTransform.position, moveSpeed);
        if ((transform.position - finishTransform.position).magnitude<= maxRadius)
            Destroy(gameObject);
    }

    public void SetFinishTransform(Transform t)
    {
        finishTransform = t;
    }
}
