using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float rotationSpeed = 1;
    public SkillsController skillsController;
    public GameObject effect; // объект со Sprite Renderer

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Transform headTransform;
    private bool arrowControl; //Переменная для определения типа управления
    private Vector2 movement;
    private float rotationZ;
    private float originalMoveSpeed;    
    private Animator animP1;
    private Animator animP2;

    private bool[] checkPoints;
    private int lastCheckpoint;
    

    private bool underEffect;
    private float effectDuration;
    private string currentSkillTag = null;
        
    void Start()
    {
        gameManager = Camera.main.GetComponent<GameManager>();

        rb = GetComponent<Rigidbody2D>();
        headTransform = transform.Find("Head").GetComponent<Transform>();
        originalMoveSpeed = moveSpeed;
        checkPoints = new bool[3];        

        string parentName = transform.name; //Получаем родительское имя

        if (parentName == "Player1") //Если это игрок1 то управление не на стрелках
        {
            arrowControl = false;
            animP1 = GetComponentInChildren<Animator>();
        }
        else
        {
            arrowControl = true;
            animP2 = GetComponentInChildren<Animator>();
        }        
        rotationZ = transform.rotation.eulerAngles.z;
        lastCheckpoint = checkPoints.Length - 1; // В начале игры выставляем индикатор последнего чекпоинта     
        
    }

    private void FixedUpdate()
    {
        if (!arrowControl && Input.GetAxisRaw("HorizontalP1") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP1") * rotationSpeed;
            rb.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (!arrowControl && Input.GetAxisRaw("VerticalP1") != 0)
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
            animP1.speed = 1;
        }

        else
        {
            if(animP1)
            animP1.speed = 0;
        }

        if (arrowControl && Input.GetAxisRaw("HorizontalP2") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP2") * rotationSpeed;
            rb.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (arrowControl && Input.GetAxisRaw("VerticalP2") != 0)
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
            animP2.speed = 1;
        }

        else
        {
            if(animP2)
            animP2.speed = 0;
        }

        if (!arrowControl && Input.GetKey(KeyCode.Q) && currentSkillTag != null) // Использование скилла первым игроком
        {
            skillsController.UseSkill(currentSkillTag, transform);
            currentSkillTag = null;
            animP1.CrossFade("Pers", 0);
            GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("Sprites/Character/Pers", typeof(Sprite)) as Sprite;
        }

        if (arrowControl && Input.GetKey(KeyCode.RightControl) && currentSkillTag != null) // Использование скилла вторым игроком
        {
            skillsController.UseSkill(currentSkillTag, transform);
            currentSkillTag = null;
            animP2.CrossFade("Pers", 0);
            GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("Sprites/Character/Pers", typeof(Sprite)) as Sprite;
        }

        if (underEffect)
        {
            if (effectDuration > 0)
                effectDuration -= 0.02f;
            else
            {
                moveSpeed = originalMoveSpeed;
                underEffect = false;
                effect.SetActive(false);
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }      

    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Bacterium" || other.gameObject.tag == "Mucus")
        {
            if (other.gameObject.GetComponent<BoostWithEffect>())
                other.gameObject.GetComponent<BoostWithEffect>().UseBoost(this);
            else if (other.gameObject.GetComponent<BacteriumController>())
                other.gameObject.GetComponent<BacteriumController>().UseBoost(this);
        }
        else if(other.gameObject.tag == "SkillBox")
        {
            skillsController.SetSkill(other.gameObject.name, this);
            Destroy(other.gameObject);
        }
    }

    public void FinishComplete()
    {
        if (checkPoints[2])
        {       
            if (!arrowControl)
                gameManager.CompleteLapP1();
            else
                gameManager.CompleteLapP2();
            ClearCheckPoints(); // Приравниваем все чекпоинты к false  
        }         
    }

    public void CheckPointComplete(int checkPointNumber)
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
            Debug.Log(transform.name +"Едет в другую сторону");
            gameManager.ShowWarning(transform.name);
        }
        else
            lastCheckpoint = checkPointNumber;
    }

    private void ClearCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
            checkPoints[i] = false;
    }

    public void AddEffect(float duration, float speedChange, Sprite sprite) // Добавление эффекта ускорения или замедления на персонажа
    {
        effectDuration = duration;
        underEffect = true;
        moveSpeed = originalMoveSpeed + speedChange;
        effect.SetActive(true);
        effect.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void AddEffect(float duration, float speedChange)
    {
        effectDuration = duration;
        underEffect = true;
        moveSpeed = originalMoveSpeed + speedChange;        
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public void AddSkill(string tag, string animName) // Добавление скилла
    {
        currentSkillTag = tag;
        if(!arrowControl)
            animP1.CrossFade(animName, 0);
        else
            animP2.CrossFade(animName, 0);
    }
}

