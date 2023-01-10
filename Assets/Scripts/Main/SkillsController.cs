using UnityEngine;

public class SkillsController : MonoBehaviour
{
    public void SetSkill(string boxName, PlayerController playerController) // Метод для определения скилла, который подобрал персонаж
    {
        if (boxName.Contains("ChainBox"))
            playerController.AddSkill("Chain", "Biker");
        else if (boxName.Contains("LassoBox"))
            playerController.AddSkill("Lasso", "Cowboy");
        else if (boxName.Contains("MedBox"))
            playerController.AddSkill("Med", "Doctor");

        AudioManager.S.PlaySound(Sounds.Buff);
    }

    public void UseSkill(string tag, Transform player) // Использование скилла, которым на данный момент владеет персонаж
    {
        if (tag == "Chain")
        {
            GameObject chainS = Instantiate(Resources.Load("Prefabs/Chain", typeof(GameObject)), player) as GameObject;
            AudioManager.S.PlaySound(Sounds.Chain);
        }

        if (tag == "Lasso")
        {
            Vector2 startPos = player.position + (player.Find("Head").transform.position - player.position) * 2f; // Определяем стартовую позицию немного спереди от персонажа
            GameObject lassoS = Instantiate(Resources.Load("Prefabs/Lasso", typeof(GameObject)), startPos, player.rotation) as GameObject;
            Vector2 direction = player.Find("Head").transform.position - player.position; // Определяем вектор направления полета лассо (куда смотрит персонаж)
            lassoS.GetComponent<LassoSkill>().SetDirectionCast(direction);
            AudioManager.S.PlaySound(Sounds.Lasso);
        }

        if (tag == "Med")
        {
            Vector2 startPos = player.position - (player.Find("Head").transform.position - player.position) * 2.5f; // Определяем стартовую позицию сзади персонажа
            GameObject medS = Instantiate(Resources.Load("Prefabs/med", typeof(GameObject)), startPos, player.rotation) as GameObject;
            AudioManager.S.PlaySound(Sounds.Antibiotic);
        }
    }
}
