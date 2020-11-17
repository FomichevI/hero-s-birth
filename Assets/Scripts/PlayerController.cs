using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;

    public float rotationSpeed = 1;

    public Rigidbody2D rb;

    public Transform zoidTransform;

    public Transform headTransform;

    private bool ArrowControl; //Переменная для определения типа управления

    private Vector2 movement;

    private float rotationZ;

    // Start is called before the first frame update
    void Start()
    {
        var parentName = transform.name; //Получаем родительское имя

        if (parentName == "P1") //Если это игрок1 то управление не на стрелках
        {
            ArrowControl = false;
        }
        else
        {
            ArrowControl = true;
        }

    }

    private void FixedUpdate()
    {
        if (ArrowControl == false && Input.GetAxisRaw("HorizontalP1") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP1") * rotationSpeed;
            rb.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (ArrowControl == false && Input.GetAxisRaw("VerticalP1") != 0)
        {
            if (Input.GetAxisRaw("VerticalP1") < 0)
            {
                movement.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP1") * moveSpeed/2; // скорость движения назад в 2 раза меньше 
                movement.x = (transform.position.x - headTransform.position.x) * Input.GetAxis("VerticalP1") * moveSpeed/2;
            }
            else
            {
                movement.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP1") * moveSpeed; 
                movement.x = (transform.position.x - headTransform.position.x) * Input.GetAxis("VerticalP1") * moveSpeed;
            }

            rb.velocity -= movement;
        }


        if (ArrowControl == true && Input.GetAxisRaw("HorizontalP2") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP2") * rotationSpeed;
            rb.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (ArrowControl == true && Input.GetAxisRaw("VerticalP2") != 0)
        {
            if (Input.GetAxisRaw("VerticalP2") < 0)
            {
                movement.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP2") * moveSpeed / 2; // скорость движения назад в 2 раза меньше 
                movement.x = (transform.position.x - headTransform.position.x) * Input.GetAxis("VerticalP2") * moveSpeed / 2;
            }
            else
            {
                movement.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP2") * moveSpeed;
                movement.x = (transform.position.x - headTransform.position.x) * Input.GetAxis("VerticalP2") * moveSpeed;
            }

            rb.velocity -= movement;
        }              
    }
}

