using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float durationSkill;
    public float durationEffect;
    public float speedChange;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "P1" || collision.gameObject.name == "P2")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().AddEffect(durationEffect, speedChange);
            Destroy(gameObject);
        }
    }
}
