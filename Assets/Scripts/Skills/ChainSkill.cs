using UnityEngine;

public class ChainSkill : Skill
{
    [SerializeField] private float _rotationSpeed = 50;   
    private Vector3 _rotation;

    private void FixedUpdate()
    {
        if(_durationSkill > 0)
        {
            _durationSkill -= 0.02f;
            _rotation.z += _rotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(_rotation);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
