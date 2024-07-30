using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private bool _doStartRound = false;

    [SerializeField]
    private int _startSpawnAmount = 5;
    [SerializeField]
    private float _spawnFrequency = 0.25f;

    private int _aliveEnemiesCount = 0;
    public int currentEnemyCount 
    {
        get => _aliveEnemiesCount;
        set => _aliveEnemiesCount = Mathf.Clamp(value, 0, 999);
    }
    private int _enemyPerRound;

    [SerializeField]
    private float _startTimer = 3f;

    [SerializeField]
    private List<Transform> _spawnPoints;

    [SerializeField]
    private Enemy[] _enemies = new Enemy[4];

    private string[] poolKeys = new string[4]
    {
        "EarthEnemy",
        "WaterEnemy",
        "FireEnemy",
        "AirEnemy"
    };

    private void Start()
    {
        _aliveEnemiesCount = _startSpawnAmount;
        _enemyPerRound = _aliveEnemiesCount;
        for (int i = 0; i < _enemies.Length; i++)
        {
            ObjectPoolManager.Instance.CreatePool(poolKeys[i], _enemies[i].gameObject, this.gameObject);
        }
    }

    private void Update()
    {
        if (_doStartRound)
        {
            StartCoroutine(StartRound());
        }
    }

    private IEnumerator StartRound()
    {
        _doStartRound = false;
        float elapsedTime = _startTimer;
        _enemyPerRound += 5;
        _aliveEnemiesCount = _enemyPerRound;
        while (elapsedTime > 0.0f)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        
        while (currentEnemyCount > 0)
        {
            int nextToSpawn = Random.Range(0, 4);
            yield return new WaitForSeconds(_spawnFrequency);
            GameObject enemy = ObjectPoolManager.Instance.GetObject(poolKeys[nextToSpawn]);
            enemy.GetComponent<Enemy>().Initialize(poolKeys[nextToSpawn]);
            
            enemy.transform.position = _spawnPoints[Random.Range(0,_spawnPoints.Count)].position;
            currentEnemyCount--;
        }
    }
}
