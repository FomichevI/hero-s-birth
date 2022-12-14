using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] BaffSpawnPoints;
    [Header ("Для точек с пометкой start")] public Transform[] BacteriumSpawnPoints1;
    [Header("Для точек с пометкой finish")] public Transform[] BacteriumSpawnPoints2;
    public float SpawnBacteriumDelay;
    public float SpawnBuffsDelay;

    private float _timeToSpawnBacterium;
    private float _timeToSpawnBuff;
    private int _lastBacteriumSpawn;
    private int _lastBuffSpawn;

    void Start()
    {
        _timeToSpawnBacterium = SpawnBacteriumDelay;
        _timeToSpawnBuff = SpawnBuffsDelay;
    }

    private void FixedUpdate()
    {
        if (_timeToSpawnBacterium > 0)
            _timeToSpawnBacterium -= 0.02f;
        else
        {
            SpawnBacterium();
            _timeToSpawnBacterium = SpawnBacteriumDelay;
        }

        if (_timeToSpawnBuff > 0)
            _timeToSpawnBuff -= 0.02f;
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
            _timeToSpawnBuff = SpawnBacteriumDelay;
        }
    }

    private void SpawnBacterium()
    {
        int rand = Random.Range(0,2); // Для рандомизации точки начала движения
        if (rand == 0)
        {
        re:
            rand = Random.Range(0, BacteriumSpawnPoints1.Length);
            if (rand == _lastBacteriumSpawn)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)) , BacteriumSpawnPoints1[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(BacteriumSpawnPoints2[rand]);
            _lastBacteriumSpawn = rand;
        }
        else
        {
        re:
            rand = Random.Range(0, BacteriumSpawnPoints1.Length);
            if (rand == _lastBacteriumSpawn)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)), BacteriumSpawnPoints2[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(BacteriumSpawnPoints1[rand]);
            _lastBacteriumSpawn = rand;
        }
    }

    private void SpawnMucus()
    {
    re:
        int rand = Random.Range(0, BaffSpawnPoints.Length);
        if (rand == _lastBuffSpawn)
            goto re;
        GameObject mucus = Instantiate(Resources.Load("Prefabs/Mucus", typeof(GameObject)), BaffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawn = rand;
    }

    private void SpawnChainBox()
    {
    re:
        int rand = Random.Range(0, BaffSpawnPoints.Length);
        if (rand == _lastBuffSpawn)
            goto re;
        GameObject chainB = Instantiate(Resources.Load("Prefabs/ChainBox", typeof(GameObject)), BaffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawn = rand;
    }

    private void SpawnLassoBox()
    {
    re:
        int rand = Random.Range(0, BaffSpawnPoints.Length);
        if (rand == _lastBuffSpawn)
            goto re;
        GameObject lassoB = Instantiate(Resources.Load("Prefabs/LassoBox", typeof(GameObject)), BaffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawn = rand;
    }

    private void SpawnMedBox()
    {
    re:
        int rand = Random.Range(0, BaffSpawnPoints.Length);
        if (rand == _lastBuffSpawn)
            goto re;
        GameObject medB = Instantiate(Resources.Load("Prefabs/MedBox", typeof(GameObject)), BaffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawn = rand;
    }
}
