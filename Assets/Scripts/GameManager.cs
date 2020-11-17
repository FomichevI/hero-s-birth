using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text counterP1Text;
    public Text counterP2Text;
    int counterP1 = 10;
    int counterP2 = 10;


    // Start is called before the first frame update
    void Start()
    {
        counterP1Text.text = counterP1.ToString();
        counterP2Text.text = counterP2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteLapP1()
    {
        counterP1 -= 1;
        counterP1Text.text = counterP1.ToString();
    }
    public void CompleteLapP2()
    {
        counterP2 -= 1;
        counterP2Text.text = counterP2.ToString();
    }
}
