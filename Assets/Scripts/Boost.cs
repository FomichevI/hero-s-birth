using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [Header("+ускорение, -замедление")] public float speedChange;

    public float durationEffect;

    public float lifeTime;
      

    private void FixedUpdate()
    {
        if (lifeTime > 0)
            lifeTime -= 0.02f;
        else
            Destroy(gameObject);
    }

    public void UseBoost(PlayerController pc)
    {
        if (gameObject.tag == "Bacterium" || gameObject.tag == "Mucus")
        {
            pc.AddEffect(durationEffect, speedChange);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "")
        {

        }
    }
}
