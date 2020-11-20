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

        if (skillTag.Contains("LassoBox"))
        {
            playerController.AddSkill("Lasso", Resources.Load("Sprites/Cowboy", typeof(Sprite)) as Sprite);
        }
    }

    public void UseSkill(string tag, Transform player) // Использование скилла, которым на данный момент владеет персонаж
    {
        if (tag == "Chain")
        {
            GameObject chainS = Instantiate(Resources.Load("Prefabs/Chain", typeof(GameObject)), player) as GameObject;
        }

        if (tag == "Lasso")
        {
            Vector2 startPos = player.position + (player.Find("Head").transform.position - player.position)*2.5f; // Определяем стартовую позицию немного спереди от персонажа
            GameObject lassoS = Instantiate(Resources.Load("Prefabs/Lasso", typeof(GameObject)), startPos, player.rotation) as GameObject;
            Vector2 direction = player.Find("Head").transform.position - player.position; // Определяем вектор направления полета лассо (куда смотрит персонаж)
            lassoS.GetComponent<LassoSkill>().SetDirectionCast(direction);
        }
    }
}
