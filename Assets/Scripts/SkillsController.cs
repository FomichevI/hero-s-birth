using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{     




    public void SetSkill(string skillTag, PlayerController playerController) // Метод для определения скилла, который подобрал персонаж
    {
        if (skillTag.Contains("ChainBox"))
        {
            playerController.AddSkill("Chain", Resources.Load("Sprites/Biker", typeof(Sprite)) as Sprite);
        }
    }

    public void UseSkill(string tag, Transform player)
    {
        if (tag == "Chain")
        {
            GameObject bact = Instantiate(Resources.Load("Prefabs/Chain", typeof(GameObject)), player) as GameObject;
        }
    }
}
