using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] baffSpawnPoints;

    [Header ("Для точек с пометкой 1")] public Transform[] bacteriumSpawnPoints1;

    [Header("Для точек с пометкой 2")] public Transform[] bacteriumSpawnPoints2;    

    public GameObject bacteriumPrefab;

    public float spawnBacteriumDelay = 5;

    private float timeToSpawnBacterium;

    void Start()
    {
        timeToSpawnBacterium = spawnBacteriumDelay;
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
    }

    private void SpawnBacterium()
    {
        int rand = Random.Range(0,2); // Для рандомизации точки начала движения
        if (rand == 0)
        {
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            GameObject bact = Instantiate(bacteriumPrefab, bacteriumSpawnPoints1[rand]);
            bact.GetComponent<BacteriumController>().finishTransform = bacteriumSpawnPoints2[rand];
        }
        else
        {
            rand = Random.Range(0, bacteriumSpawnPoints1.Length);
            GameObject bact = Instantiate(bacteriumPrefab, bacteriumSpawnPoints2[rand]);
            bact.GetComponent<BacteriumController>().finishTransform = bacteriumSpawnPoints1[rand];
        }
    }
}
