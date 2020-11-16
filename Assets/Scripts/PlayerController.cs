using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    
    private bool ArrowControl; //Переменная для определения типа управления

    private Vector2 movement;

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

    }

    private void Update()
    {
        if (ArrowControl == false && Input.GetAxisRaw("HorizontalP1") != 0)
        {
            movement.x = Input.GetAxisRaw("HorizontalP1");
        }

        if (ArrowControl == false && Input.GetAxisRaw("VerticalP1") != 0)
        {
            movement.y = Input.GetAxis("VerticalP1");

        }


        if (ArrowControl == true && Input.GetAxisRaw("HorizontalP2") != 0)
        {
            movement.x = Input.GetAxisRaw("HorizontalP2");
        }

        if (ArrowControl == true && Input.GetAxisRaw("VerticalP2") != 0)
        {
            movement.y = Input.GetAxis("VerticalP2");

        }

        Debug.Log(movement);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}

