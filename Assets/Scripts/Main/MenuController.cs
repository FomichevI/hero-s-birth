using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private GameObject _controllingPanel;
    [SerializeField] private GameObject _engCheckBoxPoint;
    [SerializeField] private GameObject _rusCheckBoxPoint;
    [SerializeField] private Slider _volumeslider;

    private void Start()
    {
        SetCheckPoints();
        _volumeslider.value = PlayerPrefs.GetFloat("VolumeValue");
        _settingsPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _descriptionPanel.SetActive(false);
        _controllingPanel.SetActive(false);
    }


    private void Update()
    {
        int i = 0;
        if (Input.GetKeyDown("escape"))
        {
            _settingsPanel.SetActive(false);
            _creditsPanel.SetActive(false);
            _descriptionPanel.SetActive(false);
            _controllingPanel.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }

    public void OpenSettings()
    {
        AudioManager.S.PlaySound(Sounds.Click);
        _settingsPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        AudioManager.S.PlaySound(Sounds.Click);
        _creditsPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OpenDescription()
    {
        AudioManager.S.PlaySound(Sounds.Click);
        _descriptionPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OpenRules()
    {
        AudioManager.S.PlaySound(Sounds.Click);
        _controllingPanel.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void Quit()
    {
        AudioManager.S.PlaySound(Sounds.Click);
        Application.Quit();
    }

    private void SetCheckPoints()
    {
        if (PlayerPrefs.GetString("Language") == "eng")
        {
            _engCheckBoxPoint.SetActive(true);
            _rusCheckBoxPoint.SetActive(false);
        }
        else if (PlayerPrefs.GetString("Language") == "rus")
        {
            _engCheckBoxPoint.SetActive(false);
            _rusCheckBoxPoint.SetActive(true);
        }
    }
}
