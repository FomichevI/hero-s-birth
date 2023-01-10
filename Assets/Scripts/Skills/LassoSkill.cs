using UnityEngine;

public class LassoSkill : Skill
{
    [SerializeField] private float _speed = 0.3f;
    private Vector2 _directionCast;
    private Vector2 _pos;

    private void FixedUpdate()
    {
        if (_durationSkill > 0)
        {
            _durationSkill -= 0.02f;
            _pos += _directionCast * _speed;
            transform.position = _pos;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDirectionCast(Vector2 direction)
    {
        _directionCast = direction;
        _pos = transform.position;
    }
}
