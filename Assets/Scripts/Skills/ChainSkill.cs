using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSkill : Skill
{
    public float rotationSpeed;
    

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


}
