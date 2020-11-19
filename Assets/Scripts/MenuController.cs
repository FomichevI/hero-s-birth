using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public Text[] EnglishText;
    public Text[] RussianText;

    public Dropdown LanguageSelector;

    public GameObject[] Menus;

    public AudioMixer Mixer;

    public Slider AudioSlider;

    private float VolumeLevel;

    private string SettingsKey = "ThisIsOurKey";

    private string Language;
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        if (Input.GetKeyDown("escape") && Menus[0].active == false)
        {
            while (i!= Menus.Length)
            {
                Menus[i].active = false;
                i++;
            }
            i = 0;
            Menus[0].active = true;
        }
        
    }
  
    public void OpenSettings()
    {
        Menus[1].active = true;
        Menus[0].active = false;
    }

    private  void LoadSettings()
    {

        Debug.Log("Getting Settings from Prefs");
        Language = PlayerPrefs.GetString(SettingsKey, Language);
        Debug.Log("Language from prefs -" + Language);
        VolumeLevel = PlayerPrefs.GetFloat(SettingsKey, VolumeLevel);
        Debug.Log("Volume Level from prefs -" + VolumeLevel);
        Debug.Log("Updating settings ... ");
        Mixer.SetFloat("Volume", VolumeLevel);
        int i = 0;
        
        if (Language != "rus")
        {
            while (i!= EnglishText.Length)
            {
                RussianText[i].enabled = false;
                EnglishText[i].enabled = true;
                i++;
            }
            i = 0;
            Debug.Log("Language is now set to English");
        }
        if (Language == "rus")
        {
            while (i != RussianText.Length)
            {
                EnglishText[i].enabled = false;
                RussianText[i].enabled = true;
                i++;
            }
            i = 0;
            Debug.Log("Language is now set to Russian");


        }
    }

    public void UpdateSettings()
    {

    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(SettingsKey, VolumeLevel);
        PlayerPrefs.SetString(SettingsKey, Language);
    }

    public void PlayMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectMode()
    {

    }

    public void Credits()
    {
        Menus[2].active = true;
        Menus[0].active = false;
    }
    public void ChangeLanguage()
    {

        Debug.Log(LanguageSelector.value);
       
        if (LanguageSelector.value == 0)
        {
            Debug.Log("English is Selected");
            Language = "eng";
            SaveSettings();
            LoadSettings();
        }
        if (LanguageSelector.value == 1)
        {
            Debug.Log("Russian is Selected");
            Language = "rus";
            SaveSettings();
            LoadSettings();
        }
    }

    public void SetVolume(float value)
    {
        Mixer.SetFloat("Volume", AudioSlider.value);
        VolumeLevel = AudioSlider.value;
        SaveSettings();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
