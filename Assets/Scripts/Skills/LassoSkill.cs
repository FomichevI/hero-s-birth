using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoSkill : Skill
{
    public float speed;

    private Vector2 directionCast;
    private Vector2 pos;

    private void FixedUpdate()
    {
        if (durationSkill > 0)
        {
            durationSkill -= 0.02f;
            pos += directionCast * speed;
            transform.position = pos;
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    public void SetDirectionCast(Vector2 direction)
    {
        directionCast = direction;
        pos = transform.position;
    }

}
