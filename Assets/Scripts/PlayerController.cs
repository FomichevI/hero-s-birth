using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody theRB;

    public float forwardAccel =8f, reverseAccel =4f, maxSpeed =50f, turnStrength = 180;

    public float speedInput, turnInput;

     private bool ArrowControl; //Переменная для определения типа управления

    // Start is called before the first frame update
    void Start()
    {
        var parentName = transform.name; //Получаем родительское имя

        if (parentName == "P1") //Если это игрок1 то управление не на стрелках
        {
            ArrowControl = false;
        }else
        {
            ArrowControl = true;
        }

        theRB.transform.parent = null;
    }

    private void Update()
    {
        //speedInput = 0f;
        if (Input.GetKeyDown(KeyCode.W) && ArrowControl == false)
        {
            speedInput = Input.GetAxis("VerticalP1") * forwardAccel * 1000f;
        } else if (Input.GetAxis("VerticalP1") < 0 && ArrowControl == false)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }


        transform.position = theRB.transform.position;
        
       // Debug.Log("SpeedInput: " + speedInput);
        if (Mathf.Abs(speedInput) > 0)
        {
            theRB.AddForce(transform.forward * forwardAccel * speedInput);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

   
}
