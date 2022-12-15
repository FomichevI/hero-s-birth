using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text CounterP1Text;
    public Text CounterP2Text;
    public GameObject WinPanel;
    public float WarningDuration = 2; // Продолжительность предупреждения о движении не в ту сторону
    

    public Text WarningTextP1rus;
    public Text WarningTextP2rus;
    public Text WarningTextP1eng;
    public Text WarningTextP2eng;

    private int _counterP1 = 0;
    private int _counterP2 = 0;
    private float _currentWarningP1;
    private float _currentWarningP2;
    private float _timer = 3.64f;
    private GameObject _countdown;


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
        _counterP1 += 1;
        CounterP1Text.text = _counterP1.ToString() + "/10";
        if (_counterP1 == 10)
        {
            if (PlayerPrefs.GetString("Language") == "eng")
            {
                CompleteGame("Player 1");
            }
            else if (PlayerPrefs.GetString("Language") == "rus")
            {
                CompleteGame("Игрок 1");
            }
        }
    }

    public  void CompleteLapP2()
    {
        _counterP2 += 1;
        CounterP2Text.text = _counterP2.ToString() + "/10";
        if (_counterP2 == 10)
        {
            if (PlayerPrefs.GetString("Language") == "eng")
            {
                CompleteGame("Player 2");
            }
            else if (PlayerPrefs.GetString("Language") == "rus")
            {
                CompleteGame("Игрок 2");
            }
        }        
    }

    private void FixedUpdate()
    {
        if(_currentWarningP1 > 0)
        {
            if (_currentWarningP1 % 0.5f > 0.25f)
            {
                WarningTextP1rus.color = Color.red;
                WarningTextP1eng.color = Color.red;
            }
            else
            {
                WarningTextP1rus.color = Color.black;
                WarningTextP1eng.color = Color.black;
            }
            _currentWarningP1 -= 0.02f;       
        }
        else
        {
            WarningTextP1rus.enabled = false;
            WarningTextP1eng.enabled = false;
        }

        if (_currentWarningP2 > 0)
        {
            if (_currentWarningP2 % 0.5f > 0.25f)
            {
                WarningTextP2rus.color = Color.red;
                WarningTextP2eng.color = Color.red;
            }
            else
            {
                WarningTextP2rus.color = Color.black;
                WarningTextP2eng.color = Color.black;
            }
            _currentWarningP2 -= 0.02f;
        }
        else
        {
            WarningTextP2rus.enabled = false;
            WarningTextP2eng.enabled = false;
        }

        if (_timer > 0)
        {
            if (_timer%1 < 0.67f && _timer % 1 > 0.65f)
                AudioManager.S.PlaySound(Sounds.Hit);
            _timer -= 0.02f;
        }
        else
            _countdown.SetActive(false);
    }

    public void CompleteGame(string playerName)
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);

        if (PlayerPrefs.GetString("Language") == "eng")
        {
            WinPanel.GetComponentInChildren<Text>().text = playerName + "\nWon!";
        }
        if (PlayerPrefs.GetString("Language") == "rus")
        {
            WinPanel.GetComponentInChildren<Text>().text = playerName + "\nПобедил!";
        }

    }

    public void RestartGame() // Полностью очищаем сцену от уровня и загружаем его заново
    {
        if(GameObject.FindWithTag("Level"))
            Destroy(GameObject.FindWithTag("Level"));
        Instantiate(Resources.Load("Prefabs/_Level_1", typeof(GameObject)), Vector3.zero, Quaternion.Euler(Vector3.zero));
        _countdown = GameObject.Find("Countdown");
        _timer = 3.64f;
        WinPanel.SetActive(false);
        RestartCounters();
        Time.timeScale = 1;
        Stop();
        Invoke("Go", 3.7f);
    }

    public void BackToMenu()
    {
        Destroy(GameObject.FindWithTag("Level"));
        WinPanel.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void RestartCounters()
    {
        _counterP1 = 0;
        _counterP2 = 0;
        CounterP1Text.text = _counterP1.ToString() + "/10";
        CounterP2Text.text = _counterP2.ToString() + "/10";
    }

    public void ShowWarning(string name)
    {
        if (name == "Player1")
        {
            if (PlayerPrefs.GetString("Language") == "rus")            
                WarningTextP1rus.enabled = true;
            else if (PlayerPrefs.GetString("Language") == "eng")
                WarningTextP1eng.enabled = true;
            _currentWarningP1 = WarningDuration;            
        }
        else
        {
            if (PlayerPrefs.GetString("Language") == "rus")
                WarningTextP2rus.enabled = true;
            else if (PlayerPrefs.GetString("Language") == "eng")
                WarningTextP2eng.enabled = true;
            _currentWarningP2 = WarningDuration;
        }
    }
}
