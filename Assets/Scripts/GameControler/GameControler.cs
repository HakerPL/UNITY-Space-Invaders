using Assets.Scripts.GameControler;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerPrefab;
    public Transform PlayerStartPosition;
    [Space(10)]

    [Header("PlayerLife")]
    public int PlayerLife;
    public SpawnLifes SpawnLifesObject;

    private PlayerSpawn _playerSpawn;
    [Space(10)]

    [Header("Enemy")]
    public GameObject EnemysPrefab;
    public float2 StartShootBetwen;
    public float TimeBetwenShoot;
    public float MoveSpeed;
    public float SpeedAddWhenDead;
    public float HowManyDeathsIncreaseSpeed;

    private int _enemyCount;

    public delegate void SetShootStatus(bool status);
    public event SetShootStatus SetShootStatusEvent;

    private EnemyControler _spawnEnemys;

    // Use this for initialization
    void Start()
    {
        LifeSpawn();

        _playerSpawn = new PlayerSpawn(PlayerPrefab, PlayerStartPosition);
        _playerSpawn.SpawnPlayer()
                    .SetPlayerDeadEvent(OnPlayerDead);

        _spawnEnemys = new EnemyControler();

        GetPlayerShootStatus();

        StartCoroutine(EnemySpawnAnimation());
    }

    private void GetPlayerShootStatus()
    {
        var shootScript = _playerSpawn.GetShootStatusEvent();
        if (shootScript != null)
            SetShootStatusEvent += shootScript;
    }

    private void GetAllEnemyShootStatus()
    {
        foreach (EnemyShoot shootEnemyScript in _spawnEnemys.GetAllEnemyShoot())
            SetShootStatusEvent += shootEnemyScript.SetShootStatys;
    }

    private void OnPlayerDead()
    {
        if (SetShootStatusEvent != null)
            SetShootStatusEvent -= _playerSpawn.GetShootStatusEvent();

        SetShootStatusEvent(false);

        StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        SpawnLifesObject.DestroyLife();

        if (SpawnLifesObject.GetCountLifes() == 0)
            yield break;

        yield return new WaitForSeconds(2f);

        _playerSpawn.SpawnPlayer()
                    .SetPlayerDeadEvent(OnPlayerDead);

        SetShootStatusEvent += _playerSpawn.GetShootStatusEvent();

        SetShootStatusEvent(true);
    }

    private IEnumerator EnemySpawnAnimation()
    {
        _spawnEnemys.CreateEnemys(EnemysPrefab)
                    .SetOnDeatEvent(OnEnemyDead)
                    .SetEnemyStartShootBetwen(StartShootBetwen)
                    .SetEnemyTimeBetwenShoot(TimeBetwenShoot);

        GetAllEnemyShootStatus();
        _enemyCount = _spawnEnemys.GetCountEnemy();

        SetShootStatusEvent(false);
        _spawnEnemys.TurnMoveScript(false);

        yield return new WaitForSeconds(1f);

        while (_spawnEnemys.GetStatusMoveEnemy())
        {
            _spawnEnemys.MoveToStartPosition();
            yield return 0;
        }

        yield return new WaitForSeconds(1f);
        _spawnEnemys.TurnMoveScript(true);
        SetShootStatusEvent(true);
    }

    private void OnEnemyDead(int Points)
    {
        if (_enemyCount == 0)
            return;

        _enemyCount--;

        if(_enemyCount == 0)
            StartCoroutine(EnemySpawnAnimation());
    }

    private void LifeSpawn()
    {
        SpawnLifesObject.Spawn(PlayerLife);
    }
}
