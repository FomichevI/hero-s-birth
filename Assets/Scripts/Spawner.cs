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
    private int lastBacteriumSpawn;
    private int lastBuffSpawn;

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
        re:
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            if (rand == lastBacteriumSpawn)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)) , bacteriumSpawnPoints1[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(bacteriumSpawnPoints2[rand]);
            lastBacteriumSpawn = rand;
        }
        else
        {
        re:
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            if (rand == lastBacteriumSpawn)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)), bacteriumSpawnPoints2[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(bacteriumSpawnPoints1[rand]);
            lastBacteriumSpawn = rand;
        }
    }

    private void SpawnMucus()
    {
    re:
        int rand = Random.Range(0, baffSpawnPoints.Length);
        if (rand == lastBuffSpawn)
            goto re;
        GameObject mucus = Instantiate(Resources.Load("Prefabs/Mucus", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
        lastBuffSpawn = rand;
    }

    private void SpawnChainBox()
    {
    re:
        int rand = Random.Range(0, baffSpawnPoints.Length);
        if (rand == lastBuffSpawn)
            goto re;
        GameObject chainB = Instantiate(Resources.Load("Prefabs/ChainBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
        lastBuffSpawn = rand;
    }

    private void SpawnLassoBox()
    {
    re:
        int rand = Random.Range(0, baffSpawnPoints.Length);
        if (rand == lastBuffSpawn)
            goto re;
        GameObject lassoB = Instantiate(Resources.Load("Prefabs/LassoBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
        lastBuffSpawn = rand;
    }

    private void SpawnMedBox()
    {
    re:
        int rand = Random.Range(0, baffSpawnPoints.Length);
        if (rand == lastBuffSpawn)
            goto re;
        GameObject medB = Instantiate(Resources.Load("Prefabs/MedBox", typeof(GameObject)), baffSpawnPoints[rand]) as GameObject;
        lastBuffSpawn = rand;
    }

}
