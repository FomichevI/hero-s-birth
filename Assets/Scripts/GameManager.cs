using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public  Text counterP1Text;
    public  Text counterP2Text;
    public  GameObject player1;
    public  GameObject player2;
    public  Vector2 p1StartPos;
    public  Vector2 p2StartPos;


    private static int counterP1;
    private static int counterP2;

    private void Start()
    {
        StartTheGame();
    }


    public  void CompleteLapP1()
    {
        counterP1 -= 1;
        counterP1Text.text = counterP1.ToString();
    }
    public  void CompleteLapP2()
    {
        counterP2 -= 1;
        counterP2Text.text = counterP2.ToString();
    }

    public  void StartTheGame()
    {
        counterP1 = 10;
        counterP2 = 10;
        counterP1Text.text = counterP1.ToString();
        counterP2Text.text = counterP2.ToString();
        player1.transform.position = p1StartPos;
        player1.transform.rotation = Quaternion.Euler(0,0,-90);
        player2.transform.position = p2StartPos;
        player2.transform.rotation = Quaternion.Euler(0, 0, 90);
    }

}
