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

    public Text[] EnglishText;
    public Text[] RussianText;

    private string Language;


    private int counterP1 = 10;
    private int counterP2 = 10;

    private void Start()
    {
        int i = 0;

        Language = PlayerPrefs.GetString("ThisIsOurKey", Language);

        if (Language != "rus")
        {
            while (i != EnglishText.Length)
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
    private void Awake()
    {
        Instantiate(Resources.Load("Prefabs/_Level_1", typeof(GameObject)), Vector3.zero, Quaternion.Euler(Vector3.zero));
        counterP1Text.text = counterP1.ToString();
        counterP2Text.text = counterP2.ToString();
    }


    public  void CompleteLapP1()
    {
        counterP1 -= 1;
        counterP1Text.text = counterP1.ToString();
        if (counterP1 == 0)
        {
            if (Language != "rus")
            {
                CompleteGame("Player 1");
            }
            if (Language == "rus")
            {
                CompleteGame("Игрок 1");
            }

        }
    }
    public  void CompleteLapP2()
    {
        counterP2 -= 1;
        counterP2Text.text = counterP2.ToString();
        if (counterP2 == 0)
        {
            if (Language != "rus")
            {
                CompleteGame("Player 2");
            }
            if (Language == "rus")
            {
                CompleteGame("Игрок 2");
            }

        }
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            CompleteGame("Игрок 1");
        }
    }

    public void CompleteGame(string playerName)
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);

        if (Language != "rus")
        {
            winPanel.GetComponentInChildren<Text>().text = "Winner:\n" + playerName;
        }
        if (Language == "rus")
        {
            winPanel.GetComponentInChildren<Text>().text = "Победил\n" + playerName;
        }

    }

    public void RestartGame() // Полностью очищаем сцену от уровня и загружаем его заново
    {
        Destroy(GameObject.FindWithTag("Level"));
        Instantiate(Resources.Load("Prefabs/_Level_1", typeof(GameObject)), Vector3.zero, Quaternion.Euler(Vector3.zero));
        winPanel.SetActive(false);
        Time.timeScale = 1; // Потом будет запускаться после обратного отсчета *********************
    }

    public void BackToMenu()
    {
        Destroy(GameObject.FindWithTag("Level"));
        winPanel.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1; // Потом будет запускаться после обратного отсчета *********************
    }

}
