using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text counterP1Text;
    public Text counterP2Text;
    public GameObject winPanel;
    public float warningDuration = 2; // Продолжительность предупреждения о движении не в ту сторону
    

    public Text warningTextP1rus;
    public Text warningTextP2rus;
    public Text warningTextP1eng;
    public Text warningTextP2eng;

    private int counterP1 = 0;
    private int counterP2 = 0;
    private float currentWarningP1;
    private float currentWarningP2;
    private float timer = 3.64f;
    private GameObject Countdown;


    private void Start()
    {
        RestartGame();
    }


    private void Stop() // Отключение всего управления
    {
        GameObject.FindGameObjectWithTag("Level").GetComponent<Spawner>().enabled = false; 

        if (PlayerPrefs.HasKey("VolumeValue"))
            GameObject.FindGameObjectWithTag("Level").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("VolumeValue");
        else
            GameObject.FindGameObjectWithTag("Level").GetComponent<AudioSource>().volume = 1;

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos)
            go.GetComponent<PlayerController>().enabled = false;
    }

    private void Go() // Включение управления
    {
        GameObject.FindGameObjectWithTag("Level").GetComponent<Spawner>().enabled = true; 
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos)
            go.GetComponent<PlayerController>().enabled = true;
    }

    public  void CompleteLapP1()
    {
        counterP1 += 1;
        counterP1Text.text = counterP1.ToString() + "/10";
        if (counterP1 == 10)
        {
            if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
            {
                CompleteGame("Player 1");
            }
            else if (PlayerPrefs.GetString("ThisIsOurKey") == "rus")
            {
                CompleteGame("Игрок 1");
            }
        }
    }

    public  void CompleteLapP2()
    {
        counterP2 += 1;
        counterP2Text.text = counterP2.ToString() + "/10";
        if (counterP2 == 10)
        {
            if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
            {
                CompleteGame("Player 2");
            }
            else if (PlayerPrefs.GetString("ThisIsOurKey") == "rus")
            {
                CompleteGame("Игрок 2");
            }
        }        
    }

    private void FixedUpdate()
    {
        if(currentWarningP1 > 0)
        {
            if (currentWarningP1 % 0.5f > 0.25f)
            {
                warningTextP1rus.color = Color.red;
                warningTextP1eng.color = Color.red;
            }
            else
            {
                warningTextP1rus.color = Color.black;
                warningTextP1eng.color = Color.black;
            }
            currentWarningP1 -= 0.02f;       
        }
        else
        {
            warningTextP1rus.enabled = false;
            warningTextP1eng.enabled = false;
        }

        if (currentWarningP2 > 0)
        {
            if (currentWarningP2 % 0.5f > 0.25f)
            {
                warningTextP2rus.color = Color.red;
                warningTextP2eng.color = Color.red;
            }
            else
            {
                warningTextP2rus.color = Color.black;
                warningTextP2eng.color = Color.black;
            }
            currentWarningP2 -= 0.02f;
        }
        else
        {
            warningTextP2rus.enabled = false;
            warningTextP2eng.enabled = false;
        }

        if (timer > 0)
        {
            if (timer%1 < 0.67f && timer % 1 > 0.65f)
                AudioManager._audioManager.PlayAudio(7);
            timer -= 0.02f;
        }
        else
            Countdown.SetActive(false);
    }


    public void CompleteGame(string playerName)
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);

        if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
        {
            winPanel.GetComponentInChildren<Text>().text = playerName + "\nWon!";
        }
        if (PlayerPrefs.GetString("ThisIsOurKey") == "rus")
        {
            winPanel.GetComponentInChildren<Text>().text = playerName + "\nПобедил!";
        }

    }

    public void RestartGame() // Полностью очищаем сцену от уровня и загружаем его заново
    {
        if(GameObject.FindWithTag("Level"))
            Destroy(GameObject.FindWithTag("Level"));
        Instantiate(Resources.Load("Prefabs/_Level_1", typeof(GameObject)), Vector3.zero, Quaternion.Euler(Vector3.zero));
        Countdown = GameObject.Find("Countdown");
        timer = 3.64f;
        winPanel.SetActive(false);
        RestartCounters();
        Time.timeScale = 1;
        Stop();
        Invoke("Go", 3.7f);
    }

    public void BackToMenu()
    {
        Destroy(GameObject.FindWithTag("Level"));
        winPanel.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void RestartCounters()
    {
        counterP1 = 0;
        counterP2 = 0;
        counterP1Text.text = counterP1.ToString() + "/10";
        counterP2Text.text = counterP2.ToString() + "/10";
    }

    public void ShowWarning(string name)
    {
        if (name == "P1")
        {
            if (PlayerPrefs.GetString("ThisIsOurKey") == "rus")            
                warningTextP1rus.enabled = true;
            else if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
                warningTextP1eng.enabled = true;
            currentWarningP1 = warningDuration;            
        }
        else
        {
            if (PlayerPrefs.GetString("ThisIsOurKey") == "rus")
                warningTextP2rus.enabled = true;
            else if (PlayerPrefs.GetString("ThisIsOurKey") == "eng")
                warningTextP2eng.enabled = true;
            currentWarningP2 = warningDuration;
        }

    }

}
