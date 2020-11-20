using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public float rotationSpeed;
    public float durationSkill;
    public float durationEffect;
    public float speedChange;

    private Vector3 rotation;

    private void FixedUpdate()
    {
        if(durationSkill > 0)
        {
            durationSkill -= 0.02f;
            rotation.z += rotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "P1" || collision.gameObject.name == "P2")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().AddEffect(durationEffect, speedChange);
            Destroy(gameObject);
        }
    }

}
