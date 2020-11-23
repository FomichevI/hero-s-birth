using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    private List <Text> engListText;
    private List <Text> rusListText;
    private string languageSettings = "ThisIsOurKey";
    private string language;

    private void Awake()
    {
        FindTexts();
        LoadSettings();
    }

    private void FindTexts()
    {
        engListText = new List<Text>();
        rusListText = new List<Text>();
        Text[] list = GameObject.FindObjectsOfType<Text>();
        foreach(Text text in list)
        {
            if (text.gameObject.name.Contains("_Text_eng"))            
                engListText.Add(text);            
            else if (text.gameObject.name.Contains("_Text_rus"))
                rusListText.Add(text);
        }
    }


    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(languageSettings))
            language = PlayerPrefs.GetString(languageSettings);
        else
        {
            language = "eng";
            PlayerPrefs.SetString(languageSettings, language);
        }

        if (language == "eng")
        {
            for (int i =0; i < rusListText.Count; i++)
            {
                rusListText[i].enabled = false;
                engListText[i].enabled = true;                
            }
            Debug.Log("Language is now set to English");
        }
        if (language == "rus")
        {
            for (int i = 0; i < rusListText.Count; i++)
            {
                engListText[i].enabled = false;
                rusListText[i].enabled = true;
            }
            Debug.Log("Language is now set to Russian");
        }
    }

    public void ChangeLanguage(int value)
    {
        AudioManager._audioManager.PlayAudio(0);
        if (value == 0)
        {
            Debug.Log("English is Selected");
            language = "eng";
            PlayerPrefs.SetString(languageSettings, language);
            LoadSettings();
        }
        if (value == 1)
        {
            Debug.Log("Russian is Selected");
            language = "rus";
            PlayerPrefs.SetString(languageSettings, language);
            LoadSettings();
        }
    }
}
