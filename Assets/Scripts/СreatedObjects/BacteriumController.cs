using UnityEngine;

public class BacteriumController : Boost
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _durationEffect;
    [SerializeField] private Sprite _slowdownEffect;

    private Transform finishTransform;

    private void Start()
    {
        _lifeTime = 10;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishTransform.position, _moveSpeed);
        if ((transform.position - finishTransform.position).magnitude<= _maxRadius)
            Destroy(gameObject);
    }

    public void SetFinishTransform(Transform t)
    {
        finishTransform = t;
    }

    public void UseBoost(PlayerController pc)
    {
        pc.AddEffect(_durationEffect, _speedChange, _slowdownEffect);
        AudioManager.S.PlaySound(Sounds.Bacterium);
        Destroy(gameObject);
    }
}
