using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsController : MonoBehaviour
{     




    public void SetSkill(string skillTag, PlayerController playerController) // Метод для определения скилла, который подобрал персонаж
    {
        if (skillTag.Contains("ChainBox"))
        {
            playerController.AddSkill("Chain", "Biker");
        }

        else if (skillTag.Contains("LassoBox"))
        {
            playerController.AddSkill("Lasso", "Cowboy");
        }

        else if (skillTag.Contains("MedBox"))
        {
            playerController.AddSkill("Med", "Doctor");
        }
        AudioManager._audioManager.PlayAudio(6);
    }

    public void UseSkill(string tag, Transform player) // Использование скилла, которым на данный момент владеет персонаж
    {
        if (tag == "Chain")
        {
            GameObject chainS = Instantiate(Resources.Load("Prefabs/Chain", typeof(GameObject)), player) as GameObject;
            AudioManager._audioManager.PlayAudio(3);
        }

        if (tag == "Lasso")
        {
            Vector2 startPos = player.position + (player.Find("Head").transform.position - player.position)*2f; // Определяем стартовую позицию немного спереди от персонажа
            GameObject lassoS = Instantiate(Resources.Load("Prefabs/Lasso", typeof(GameObject)), startPos, player.rotation) as GameObject;
            Vector2 direction = player.Find("Head").transform.position - player.position; // Определяем вектор направления полета лассо (куда смотрит персонаж)
            lassoS.GetComponent<LassoSkill>().SetDirectionCast(direction);
            AudioManager._audioManager.PlayAudio(4);
        }

        if (tag == "Med")
        {
            Vector2 startPos = player.position - (player.Find("Head").transform.position - player.position) * 2.5f; // Определяем стартовую позицию сзади персонажа
            GameObject medS = Instantiate(Resources.Load("Prefabs/med", typeof(GameObject)), startPos, player.rotation) as GameObject;
            AudioManager._audioManager.PlayAudio(5);
        }
    }
}
