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

    private bool underEffect;

    private float effectDuration;

    private SkillsController currentSkill;

    private float originalMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        headTransform = transform.FindChild("Head").GetComponent<Transform>();
        originalMoveSpeed = moveSpeed;
        checkPoints = new bool[3];

        var parentName = transform.name; //Получаем родительское имя

        if (parentName == "P1") //Если это игрок1 то управление не на стрелках
        {
            ArrowControl = false;
            lastCheckpoint = checkPoints.Length - 1; // В начале игры выставляем индикатор последнего чекпоинта   
        }
        else
        {
            ArrowControl = true;
            lastCheckpoint = 0 ; // В начале игры выставляем индикатор последнего чекпоинта   
        }        
        rotationZ = transform.rotation.eulerAngles.z;
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

        if (underEffect)
        {
            if (effectDuration > 0)
                effectDuration -= 0.02f;
            else
            {
                moveSpeed = originalMoveSpeed;
                underEffect = false;               
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Start1" && !ArrowControl && checkPoints[2])
        {
            ClearCheckPoints(); // Приравниваем все чекпоинты к false   
            gameManager.CompleteLapP1();
        }

        else if (other.gameObject.name == "Start2" && ArrowControl && checkPoints[2])
        {
            ClearCheckPoints(); // Приравниваем все чекпоинты к false   
            gameManager.CompleteLapP2();
        }

        else if (other.gameObject.name == "CheckPoint1") // Для второго игрока поледовательность чекпоинтов следующая: 2-3-1-финиш
        {
            if (!ArrowControl)
                CheckPointComplete(0);
            else
                CheckPointComplete(2);
        }

        else if (other.gameObject.name == "CheckPoint2")
        {
            if (!ArrowControl)
                CheckPointComplete(1);
            else
                CheckPointComplete(0);
        }

        else if (other.gameObject.name == "CheckPoint3")
        {
            if (!ArrowControl)
                CheckPointComplete(2);
            else
                CheckPointComplete(1);
        }

        else if (other.gameObject.tag == "Bacterium" || other.gameObject.tag == "Mucus")
        {
            other.gameObject.GetComponent<Boost>().UseBoost(this);
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

    public void AddEffect(float duration, float speedChange) // Добавление эффекта ускорения или замедления на персонажа
    {
        effectDuration = duration;
        underEffect = true;
        moveSpeed = originalMoveSpeed + speedChange;
    }

    public void AddSkill(string tag) // Добавление скила
    {
        currentSkill = new SkillsController(tag);
    }
}

