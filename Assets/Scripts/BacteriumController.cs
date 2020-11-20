using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriumController : Boost
{      
    public float moveSpeed;
    public float maxRadius;
    public float durationEffect;
    public Sprite slowdownEffect;

    private Transform finishTransform;

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

    public void UseBoost(PlayerController pc)
    {
        pc.AddEffect(durationEffect, speedChange, slowdownEffect);
        Destroy(gameObject);
    }
}
