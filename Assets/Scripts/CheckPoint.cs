using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Для первого игрока поледовательность чекпоинтов следующая: 0-1-2-финиш
    // Для второго игрока поледовательность чекпоинтов следующая: 1-2-0-финиш

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "P1")
        {
            if (gameObject.name == "CheckPoint0")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(0);
            else if(gameObject.name == "CheckPoint1")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(1);
            else if (gameObject.name == "CheckPoint2")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(2);
            else if(gameObject.name == "Start1")
                collision.gameObject.GetComponent<PlayerController>().FinishComplete();
        }
        else if (collision.gameObject.name == "P2")
        {
            if (gameObject.name == "CheckPoint0")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(2);
            else if (gameObject.name == "CheckPoint1")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(0);
            else if (gameObject.name == "CheckPoint2")
                collision.gameObject.GetComponent<PlayerController>().CheckPointComplete(1);
            else if (gameObject.name == "Start2")
                collision.gameObject.GetComponent<PlayerController>().FinishComplete();
        }
    }
}
