using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _baffSpawnPoints;
    [Header ("Для точек с пометкой start")] [SerializeField] private Transform[] _bacteriumSpawnPoints1;
    [Header("Для точек с пометкой finish")] [SerializeField] private Transform[] _bacteriumSpawnPoints2;
    [SerializeField] private float _spawnBacteriumDelay = 3;
    [SerializeField] private float _spawnBuffsDelay = 4;

    private float _timeToSpawnBacterium;
    private float _timeToSpawnBuff;
    private int _lastBacteriumSpawnIndex;
    private int _lastBuffSpawnIndex;

    void Start()
    {
        _timeToSpawnBacterium = _spawnBacteriumDelay;
        _timeToSpawnBuff = _spawnBuffsDelay;
    }

    private void FixedUpdate()
    {
        if (_timeToSpawnBacterium > 0)
            _timeToSpawnBacterium -= 0.02f;
        else
        {
            SpawnBacterium();
            _timeToSpawnBacterium = _spawnBacteriumDelay;
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
            _timeToSpawnBuff = _spawnBacteriumDelay;
        }
    }

    private void SpawnBacterium()
    {
        int rand = Random.Range(0,2); // Для рандомизации точки начала движения
        if (rand == 0)
        {
        re:
            rand = Random.Range(0, _bacteriumSpawnPoints1.Length);
            if (rand == _lastBacteriumSpawnIndex)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)) , _bacteriumSpawnPoints1[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(_bacteriumSpawnPoints2[rand]);
            _lastBacteriumSpawnIndex = rand;
        }
        else
        {
        re:
            rand = Random.Range(0, _bacteriumSpawnPoints1.Length);
            if (rand == _lastBacteriumSpawnIndex)
                goto re;
            GameObject bact = Instantiate(Resources.Load("Prefabs/Bacterium", typeof(GameObject)), _bacteriumSpawnPoints2[rand]) as GameObject;
            bact.GetComponent<BacteriumController>().SetFinishTransform(_bacteriumSpawnPoints1[rand]);
            _lastBacteriumSpawnIndex = rand;
        }
    }

    private void SpawnMucus()
    {
    re:
        int rand = Random.Range(0, _baffSpawnPoints.Length);
        if (rand == _lastBuffSpawnIndex)
            goto re;
        GameObject mucus = Instantiate(Resources.Load("Prefabs/Mucus", typeof(GameObject)), _baffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawnIndex = rand;
    }

    private void SpawnChainBox()
    {
    re:
        int rand = Random.Range(0, _baffSpawnPoints.Length);
        if (rand == _lastBuffSpawnIndex)
            goto re;
        GameObject chainB = Instantiate(Resources.Load("Prefabs/ChainBox", typeof(GameObject)), _baffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawnIndex = rand;
    }

    private void SpawnLassoBox()
    {
    re:
        int rand = Random.Range(0, _baffSpawnPoints.Length);
        if (rand == _lastBuffSpawnIndex)
            goto re;
        GameObject lassoB = Instantiate(Resources.Load("Prefabs/LassoBox", typeof(GameObject)), _baffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawnIndex = rand;
    }

    private void SpawnMedBox()
    {
    re:
        int rand = Random.Range(0, _baffSpawnPoints.Length);
        if (rand == _lastBuffSpawnIndex)
            goto re;
        GameObject medB = Instantiate(Resources.Load("Prefabs/MedBox", typeof(GameObject)), _baffSpawnPoints[rand]) as GameObject;
        _lastBuffSpawnIndex = rand;
    }
}
