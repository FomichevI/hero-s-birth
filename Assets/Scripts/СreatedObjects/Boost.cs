using UnityEngine;

public class Boost : MonoBehaviour
{
    [Header("+ускорение, -замедление")] [SerializeField] protected float _speedChange;
    [SerializeField] protected float _lifeTime;

    private void FixedUpdate()
    {
        if (_lifeTime > 0)
            _lifeTime -= 0.02f;
        else
            Destroy(gameObject);
    }
}
