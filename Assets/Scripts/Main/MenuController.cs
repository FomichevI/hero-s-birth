using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{    
    public GameObject[] Menus;
    public GameObject[] checkBoxiPoints;
    public Slider volumeslider;

    private void Start()
    {
        SetCheckPoints();
        volumeslider.value = PlayerPrefs.GetFloat("VolumeValue");
        for ( int i = 1; i < Menus.Length; i++)
        {
            Menus[i].SetActive(false);
        }
    }


    void Update()
    {
        int i = 0;
        if (Input.GetKeyDown("escape") && Menus[0].active == false)
        {
            while (i!= Menus.Length)
            {
                Menus[i].SetActive(false);
                i++;
            }
            i = 0;
            Menus[0].SetActive(true);
        }
        
    }
  
    public void OpenSettings()
    {
        AudioManager._audioManager.PlayAudio(0);
        Menus[1].SetActive(true);
        Menus[0].SetActive(false);
    }    

    public void Play()
    {
        SceneManager.LoadScene(1);        
    }

    public void OpenCredits()
    {
        AudioManager._audioManager.PlayAudio(0);
        Menus[2].SetActive(true);
        Menus[0].SetActive(false);
    }

    public void OpenDescription()
    {
        AudioManager._audioManager.PlayAudio(0);
        Menus[3].SetActive(true);
        Menus[0].SetActive(false);
    }

    public void OpenRules()
    {
        AudioManager._audioManager.PlayAudio(0);
        Menus[4].SetActive(true);
        Menus[0].SetActive(false);
    }
       
    public void Quit()
    {
        AudioManager._audioManager.PlayAudio(0);
        Application.Quit();
    }

    private void SetCheckPoints()
    {
        if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
        {
            checkBoxiPoints[0].SetActive(true);
            checkBoxiPoints[1].SetActive(false);
        }
        else if(PlayerPrefs.GetString("ThisIsOurKey") == "rus")
        {
            checkBoxiPoints[0].SetActive(false);
            checkBoxiPoints[1].SetActive(true);
        }
    }

}
