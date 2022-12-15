using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float _durationSkill;
    [SerializeField] private float _durationEffect;
    [SerializeField] private float _speedChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            AudioManager.S.PlaySound(Sounds.Hit);
            collision.gameObject.GetComponentInParent<PlayerController>().AddEffect(_durationEffect, _speedChange);
            Destroy(gameObject);
        }
    }
}
