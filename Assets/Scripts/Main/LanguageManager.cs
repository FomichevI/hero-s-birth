using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Players { Player1, Player2 }

public class LanguageManager : MonoBehaviour
{
    private List<Text> _engListText;
    private List<Text> _rusListText;
    private string _languageSettings = "Language"; // Название переменной в PlayerPrefs
    private string _language; // Значение переменной из PlayerPrefs

    private void Awake()
    {
        FindTexts();
        LoadSettings();
    }

    private void FindTexts()
    {
        _engListText = new List<Text>();
        _rusListText = new List<Text>();
        Text[] list = FindObjectsOfType<Text>();
        foreach (Text text in list)
        {
            if (text.gameObject.name.Contains("_Text_eng"))
                _engListText.Add(text);
            else if (text.gameObject.name.Contains("_Text_rus"))
                _rusListText.Add(text);
        }
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(_languageSettings))
            _language = PlayerPrefs.GetString(_languageSettings);
        else
        {
            _language = "eng";
            PlayerPrefs.SetString(_languageSettings, _language);
        }

        if (_language == "eng")
        {
            for (int i = 0; i < _rusListText.Count; i++)
            {
                _rusListText[i].enabled = false;
                _engListText[i].enabled = true;
            }
            Debug.Log("Language is now set to English");
        }
        if (_language == "rus")
        {
            for (int i = 0; i < _rusListText.Count; i++)
            {
                _engListText[i].enabled = false;
                _rusListText[i].enabled = true;
            }
            Debug.Log("Language is now set to Russian");
        }
    }

    public void ChangeLanguage()
    {
        AudioManager.S.PlaySound(Sounds.Click);

        if (_language != "eng")
        {
            Debug.Log("English is selected");
            _language = "eng";
            PlayerPrefs.SetString(_languageSettings, _language);
            LoadSettings();
        }
        else if (_language != "rus")
        {
            Debug.Log("Russian is selected");
            _language = "rus";
            PlayerPrefs.SetString(_languageSettings, _language);
            LoadSettings();
        }
    }

    public string GetPlayerName(Players player)
    {
        if (player == Players.Player1)
        {
            if (PlayerPrefs.GetString("Language") == "eng")
                return "Player 1";
            else
                return "Игрок 1";
        }
        else
        {
            if (PlayerPrefs.GetString("Language") == "eng")
                return "Player 2";
            else
                return "Игрок 2";
        }
    }
}
