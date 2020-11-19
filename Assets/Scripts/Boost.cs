using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [Header("+ускорение, -замедление")] public float speedChange;

    public float durationEffect;

    public float lifeTime;

    public Sprite speedupSprite;

    public Sprite slowdownSprite;

    private void FixedUpdate()
    {
        if (lifeTime > 0)
            lifeTime -= 0.02f;
        else
            Destroy(gameObject);
    }

    public void UseBoost(PlayerController pc)
    {
        if (gameObject.tag == "Bacterium")
        {
            pc.AddEffect(durationEffect, speedChange, slowdownSprite);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Mucus")
        {
            pc.AddEffect(durationEffect, speedChange, speedupSprite);
            Destroy(gameObject);
        }

        else if (gameObject.tag == "")
        {

        }
    }
}
