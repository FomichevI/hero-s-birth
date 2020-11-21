using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] baffSpawnPoints;

    [Header ("Для точек с пометкой 1")] public Transform[] bacteriumSpawnPoints1;

    [Header("Для точек с пометкой 2")] public Transform[] bacteriumSpawnPoints2;    

    public float spawnBacteriumDelay;

    public float spawnBuffsDelay;

    private float timeToSpawnBacterium;

    private float timeToSpawnBuff;

    void Start()
    {
        timeToSpawnBacterium = spawnBacteriumDelay;
        timeToSpawnBuff = spawnBuffsDelay;
    }

    private void FixedUpdate()
    {
        if (timeToSpawnBacterium > 0)
            timeToSpawnBacterium -= 0.02f;
        else
        {
            SpawnBacterium();
            timeToSpawnBacterium = spawnBacteriumDelay;
        }

        if (timeToSpawnBuff > 0)
            timeToSpawnBuff -= 0.02f;
        else
        {
            int rand = Random.Range(0, 100); // Рандомно спавним один из существующих бустов
            if (rand < 55)
                SpawnMucus();
            else if (rand < 70)
                SpawnChainBox();
            else if (rand < 85)
                SpawnLassoBox();
            else
                SpawnMedBox();
            timeToSpawnBuff = spawnBacteriumDelay;
        }
    }

    private void SpawnBacterium()
    {
        int rand = Random.Range(0,2); // Для рандомизации точки начала движения
        if (rand == 0)
        {
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)) , bacteriumSpawnPoints1[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(bacteriumSpawnPoints2[rand]);
        }
        else
        {
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)), bacteriumSpawnPoints2[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(bacteriumSpawnPoints1[rand]);
        }
    }

    private void SpawnMucus()
    {
        int rand = Random.Range(0, baffSpawnPoints.Length);
        GameObject mucus = Instantiate(Resources.Load("Prefabs/Mucus", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
    }

    private void SpawnChainBox()
    {
        int rand = Random.Range(0, baffSpawnPoints.Length);
        GameObject chainB = Instantiate(Resources.Load("Prefabs/ChainBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
    }

    private void SpawnLassoBox()
    {
        int rand = Random.Range(0, baffSpawnPoints.Length);
        GameObject lassoB = Instantiate(Resources.Load("Prefabs/LassoBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
    }

    private void SpawnMedBox()
    {
        int rand = Random.Range(0, baffSpawnPoints.Length);
        GameObject medB = Instantiate(Resources.Load("Prefabs/MedBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
    }

}
