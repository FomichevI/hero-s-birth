using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriumController : MonoBehaviour
{   
    public Transform finishTransform;

    public float moveSpeed;

    public float maxRadius;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishTransform.position, moveSpeed);
        if ((transform.position - finishTransform.position).magnitude<= maxRadius)
            Destroy(gameObject);
    }
}
