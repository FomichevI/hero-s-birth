using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [Header("+ускорение, -замедление")] public float speedChange;


    public float lifeTime;


    private void FixedUpdate()
    {
        if (lifeTime > 0)
            lifeTime -= 0.02f;
        else
            Destroy(gameObject);
    }

  
}
