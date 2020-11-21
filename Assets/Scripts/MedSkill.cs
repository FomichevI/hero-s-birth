using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedSkill : Skill
{
    private void FixedUpdate()
    {
        if (durationSkill > 0)
        {
            durationSkill -= 0.02f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
