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

    private int _currentAmount = 0;

    public int currentEnemyCount 
    {
        get => _currentAmount;
        set => _currentAmount = Mathf.Clamp(value, 0, 999);
    }
    private int _enemyPerRound;

    [SerializeField]
    private float _startTimer = 3f;

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
        _currentAmount = _startSpawnAmount;
        _enemyPerRound = _currentAmount;
        for (int i = 0; i < _enemies.Length; i++)
        {
            ObjectPoolManager.Instance.CreatePool(poolKeys[i], _enemies[i].gameObject, this.gameObject);
        }
    }

    private void EliminateEnemy()
    {

    }

    private IEnumerator StartRound()
    {
        float elapsedTime = _startTimer;
        _enemyPerRound += 5;
        _currentAmount = _enemyPerRound;
        while (elapsedTime > 0.0f)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        
        yield return null;
    }
}
