using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public float rotationSpeed;

    public Transform zoidTransform;    

    public Transform headTransform;

    public Vector2 vector2;

    public Rigidbody2D rb;

    private bool ArrowControl; //Переменная для определения типа управления 

    private float rotationZ;    


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
        rotationZ = transform.rotation.z;
    }

    private void FixedUpdate()
    {
        if (ArrowControl == false && Input.GetAxis("HorizontalP1") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP1") * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (ArrowControl == false && Input.GetAxis("VerticalP1") != 0)
        {
            Vector2 vec = new Vector2();

            if (Input.GetAxis("VerticalP1") < 0)          
                vec.y  = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP1") * moveSpeed / 2; //Скорость движения назад в 2 раза меньше, чем вперед                           
            else
                vec.y = (transform.position.y - headTransform.position.y) * Input.GetAxis("VerticalP1") * moveSpeed;
            rb.velocity -= vec;
        }             

        if (ArrowControl == true && Input.GetAxis("HorizontalP2") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP2") * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }

        if (ArrowControl == true && Input.GetAxis("VerticalP2") != 0)
        {
            //if (Input.GetAxis("VerticalP2") < 0)
            //    rb.velocity -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP2") * moveSpeed / 2;
            //else
            //    rb.velocity -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP2") * moveSpeed;
        }



    }


}

