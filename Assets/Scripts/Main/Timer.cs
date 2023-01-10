using UnityEngine;

public class Timer : MonoBehaviour
{
    public void PlayHit()
    {
        AudioManager.S.PlaySound(Sounds.Hit);
    }
}
