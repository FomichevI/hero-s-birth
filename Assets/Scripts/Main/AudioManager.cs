using UnityEngine;

public enum Sounds { Click, SpeedUp, Bacterium, Chain, Lasso, Antibiotic, Buff, Hit, Theme };

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;

    [SerializeField] private AudioClip _clickAc;
    [SerializeField] private AudioClip _speedUpAc;
    [SerializeField] private AudioClip _bacteriumAc;
    [SerializeField] private AudioClip _chainAc;
    [SerializeField] private AudioClip _lassoAc;
    [SerializeField] private AudioClip _antibioticAc;
    [SerializeField] private AudioClip _buffAc;
    [SerializeField] private AudioClip _hitAc;
    [SerializeField] private AudioClip _themeAc;

    private void Awake()
    {
        if (S == null)
            S = this;
        if (PlayerPrefs.HasKey("VolumeValue"))
            SetVolue(PlayerPrefs.GetFloat("VolumeValue"));
        else
            SetVolue(1);
    }
    public void PlaySound(Sounds soundName)
    {
        if (soundName == Sounds.Click)
        {
            if (_clickAc) GetComponent<AudioSource>().PlayOneShot(_clickAc);
        }
        else if (soundName == Sounds.SpeedUp)
        {
            if (_speedUpAc) GetComponent<AudioSource>().PlayOneShot(_speedUpAc);
        }
        else if (soundName == Sounds.Bacterium)
        {
            if (_bacteriumAc) GetComponent<AudioSource>().PlayOneShot(_bacteriumAc);
        }
        else if (soundName == Sounds.Chain)
        {
            if (_chainAc) GetComponent<AudioSource>().PlayOneShot(_chainAc);
        }
        else if (soundName == Sounds.Lasso)
        {
            if (_lassoAc) GetComponent<AudioSource>().PlayOneShot(_lassoAc);
        }
        else if (soundName == Sounds.Antibiotic)
        {
            if (_antibioticAc) GetComponent<AudioSource>().PlayOneShot(_antibioticAc);
        }
        else if (soundName == Sounds.Buff)
        {
            if (_buffAc) GetComponent<AudioSource>().PlayOneShot(_buffAc);
        }
        else if (soundName == Sounds.Hit)
        {
            if (_hitAc) GetComponent<AudioSource>().PlayOneShot(_hitAc);
        }
        else if (soundName == Sounds.Theme)
        {
            if (_themeAc) GetComponent<AudioSource>().PlayOneShot(_themeAc);
        }
    }

    public void SetVolue(float vol)
    {
        PlayerPrefs.SetFloat("VolumeValue", vol);
        S.GetComponent<AudioSource>().volume = vol;
    }
}
