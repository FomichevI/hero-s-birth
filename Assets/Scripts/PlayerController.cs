using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public float rotationSpeed;

    public Transform zoidTransform;    

    public Transform headTransform;   
    
    private bool ArrowControl; //Переменная для определения типа управления

    private Vector3 position;

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
        position = transform.position;
        rotationZ = transform.rotation.z;
    }

    private void Update()
    {
        if (ArrowControl == false && Input.GetAxis("HorizontalP1") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP1") * rotationSpeed;
        }

        if (ArrowControl == false && Input.GetAxis("VerticalP1") != 0)
        {
            if(Input.GetAxis("VerticalP1")<0)
                position -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP1") * moveSpeed / 20;
            else
                position -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP1") * moveSpeed/10;
        }             

        if (ArrowControl == true && Input.GetAxis("HorizontalP2") != 0)
        {
            rotationZ -= Input.GetAxis("HorizontalP2") * rotationSpeed;
        }

        if (ArrowControl == true && Input.GetAxis("VerticalP2") != 0)
        {
            if (Input.GetAxis("VerticalP2") < 0)
                position -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP2") * moveSpeed / 20;
            else
                position -= (transform.position - headTransform.position) * Input.GetAxis("VerticalP2") * moveSpeed / 10;
        }



    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        transform.position = position;
    }

}

