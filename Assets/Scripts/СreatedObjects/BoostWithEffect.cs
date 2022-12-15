using UnityEngine;

public class BoostWithEffect : Boost
{
    [SerializeField] private float _durationEffect;
    [SerializeField] private Sprite _speedupEffect;

    public void UseBoost(PlayerController pc)
    {
        pc.AddEffect(_durationEffect, _speedChange, _speedupEffect);
        AudioManager.S.PlaySound(Sounds.SpeedUp);
        Destroy(gameObject);
    }
}