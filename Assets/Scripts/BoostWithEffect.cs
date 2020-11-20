using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostWithEffect : Boost
{
    public float durationEffect;
    public Sprite speedupEffect;

    public void UseBoost(PlayerController pc)
    {
        pc.AddEffect(durationEffect, speedChange, speedupEffect);
        Destroy(gameObject);
    }
}