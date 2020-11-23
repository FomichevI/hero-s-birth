using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public Transform player;    
    public float showDelay;
    public float showDuration;
    public float upperPlayer = 20;

    private float currentTime;
    private Vector2 pos;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        currentTime = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        pos.x = player.position.x;
        pos.y = player.position.y + upperPlayer;
        gameObject.transform.position = pos;

        if (currentTime % showDelay < showDuration)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.clear;
        }

        if (currentTime == float.MaxValue)
            currentTime = 0;
        currentTime += 0.02f;
    }

}
