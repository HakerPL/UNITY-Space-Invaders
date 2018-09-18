using Assets.Scripts.GameControler;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float MoveDownSpeed;
    public float SpeedAddWhenDead;
    public float HowManyDeathsIncreaseSpeed;

    private int _enemyCount;
    private int _deadEnemy;

    public delegate void SetShootStatus(bool status);
    public event SetShootStatus SetShootStatusEvent;

    private EnemyControler _enemyCotroler;
    [Space(10)]

    [Header("Point")]
    public Text PointObject;
    private int _point;
    private PointControler _pointControler;

    // Use this for initialization
    void Start()
    {
        LifeSpawn();

        _playerSpawn = new PlayerSpawn(PlayerPrefab, PlayerStartPosition);
        _playerSpawn.SpawnPlayer()
                    .SetPlayerDeadEvent(OnPlayerDead);

        _enemyCotroler = new EnemyControler();
        _point = 0;
        _pointControler = new PointControler(PointObject);
        _pointControler.UpdatePoint(_point);

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
        foreach (EnemyShoot shootEnemyScript in _enemyCotroler.GetAllEnemyShoot())
            SetShootStatusEvent += shootEnemyScript.SetShootStatys;
    }

    private void OnPlayerDead()
    {
        if (SetShootStatusEvent != null)
            SetShootStatusEvent -= _playerSpawn.GetShootStatusEvent();

        SetShootStatusEvent(false);

        SpawnLifesObject.DestroyLife();

        if (SpawnLifesObject.GetCountLifes() == 0)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(2f);

        _playerSpawn.SpawnPlayer()
                    .SetPlayerDeadEvent(OnPlayerDead);

        SetShootStatusEvent += _playerSpawn.GetShootStatusEvent();

        SetShootStatusEvent(true);
    }

    private IEnumerator EnemySpawnAnimation()
    {
        _deadEnemy = 0;
        _enemyCotroler.CreateEnemys(EnemysPrefab)
                    .SetOnDeatEvent(OnEnemyDead)
                    .SetEnemyStartShootBetwen(StartShootBetwen)
                    .SetEnemyTimeBetwenShoot(TimeBetwenShoot)
                    .AddSpeedEnemy(MoveSpeed)
                    .SetMoveDownSpeed(MoveDownSpeed);

        GetAllEnemyShootStatus();
        _enemyCount = _enemyCotroler.GetCountEnemy();

        SetShootStatusEvent(false);
        _enemyCotroler.TurnMoveScript(false);

        yield return new WaitForSeconds(1f);

        while (_enemyCotroler.GetStatusMoveEnemy())
        {
            _enemyCotroler.MoveToStartPosition();
            yield return 0;
        }

        yield return new WaitForSeconds(1f);
        _enemyCotroler.TurnMoveScript(true);
        SetShootStatusEvent(true);
    }

    private void OnEnemyDead(int points)
    {
        if (_enemyCount == 0)
            return;

        _enemyCount--;

        _point += points;
        _pointControler.UpdatePoint(_point);

        _deadEnemy++;

        if (_enemyCount == 0)
            StartCoroutine(EnemySpawnAnimation());
        else if (_deadEnemy % HowManyDeathsIncreaseSpeed == 0)
            _enemyCotroler.AddSpeedEnemy(SpeedAddWhenDead);
    }

    private void LifeSpawn()
    {
        SpawnLifesObject.Spawn(PlayerLife);
    }
}
