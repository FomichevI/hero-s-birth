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
        AudioManager._audioManager.PlayAudio(1);
        Destroy(gameObject);
    }
}