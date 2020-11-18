using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;

    public float rotationSpeed = 1;

    public GameManager gameManager;

    private Rigidbody2D rb;    

    private Transform headTransform;    

    private bool ArrowControl; //Переменная для определения типа управления

    private Vector2 movement;

    private float rotationZ;

    private bool[] checkPoints;

    private int lastCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        headTransform = transform.FindChild("Head").GetComponent<Transform>();

        var parentName = transform.name; //Получаем родительское имя

        if (parentName == "P1") //Если это игрок1 то управление не на стрелках
        {
            ArrowControl = false;
        }
        else
        {
            ArrowControl = true;
        }
        checkPoints = new bool[3];
        rotationZ = transform.rotation.eulerAngles.z;
        lastCheckpoint = checkPoints.Length - 1; // В начале игры выставляем индикатор последнего чекпоинта
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
                movement.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP1") * moveSpeed / 2; // скорость движения назад в 2 раза меньше 
                movement.x = (transform.position.x - headTransform.position.x) * Input.GetAxis("VerticalP1") * moveSpeed / 2;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Start1" && !ArrowControl && checkPoints[2])
        {
            ClearCheckPoints(); // Приравниваем все чекпоинты к false   
            gameManager.CompleteLapP1();
        }

        if (other.gameObject.name == "Start2" && ArrowControl && checkPoints[2])
        {
            ClearCheckPoints(); // Приравниваем все чекпоинты к false   
            gameManager.CompleteLapP2();
        }

        if (other.gameObject.name == "CheckPoint1") // Для второго игрока поледовательность чекпоинтов следующая: 2-3-1-финиш
        {
            if (!ArrowControl)
                CheckPointComplete(0);
            else
                CheckPointComplete(2);
        }

        if (other.gameObject.name == "CheckPoint2")
        {
            if (!ArrowControl)
                CheckPointComplete(1);
            else
                CheckPointComplete(0);
        }

        if (other.gameObject.name == "CheckPoint3")
        {
            if (!ArrowControl)
                CheckPointComplete(2);
            else
                CheckPointComplete(1);
        }
    }

    private void CheckPointComplete(int checkPointNumber)
    {
        if (checkPointNumber == 0)
            checkPoints[checkPointNumber] = true;
        else if (checkPoints[checkPointNumber - 1])
            checkPoints[checkPointNumber] = true;

        // Проверка на направление движения

        if (checkPointNumber == lastCheckpoint)
        {
            if (checkPointNumber == 0)
                lastCheckpoint = checkPoints.Length - 1;
            else
                lastCheckpoint = checkPointNumber - 1;
            Debug.Log(transform.name + "Едет в другую сторону");
        }
        else
            lastCheckpoint = checkPointNumber;
    }

    private void ClearCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
            checkPoints[i] = false;
    }
}

