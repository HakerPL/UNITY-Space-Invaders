using Assets.Scripts.GameControler;
using System.Collections;
using System.Collections.Generic;
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

    // Use this for initialization
    void Start ()
    {
        LifeSpawn();

        _playerSpawn = new PlayerSpawn(PlayerPrefab, PlayerStartPosition);
        _playerSpawn.SpawnPlayer(OnPlayerDead);
    }

    private void OnPlayerDead()
    {
        StartCoroutine(PlayerDead());
    }

    private IEnumerator PlayerDead()
    {
        SpawnLifesObject.DestroyLife();

        if (SpawnLifesObject.GetCountLifes() == 0)
            yield break;

        yield return new WaitForSeconds(2f);
        _playerSpawn.SpawnPlayer(OnPlayerDead);
    }

    private void LifeSpawn()
    {
        SpawnLifesObject.Spawn(PlayerLife);
    }
}
