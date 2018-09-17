using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static EnemyDeath;

public class EnemyControler : MonoBehaviour
{
    private List<GameObject> _child;
    private bool _enemyMoveToStartPosition = true;

    private GameObject _enemyInstance;
    private Vector3 _startPosition = new Vector3(0, 0, 0);
    List<EnemyShoot> _enemyShootScript;
    EnemyMove _enemyMoveScript;

    #region Builder

    public EnemyControler CreateEnemys(GameObject enemysPrefab)
    {
        if (_enemyInstance != null)
            Destroy(_enemyInstance);

        _enemyMoveToStartPosition = true;
        _enemyInstance = Instantiate(enemysPrefab);
        GetAllChild(_enemyInstance);
        GetAllChildEnemyShoot();
        _enemyMoveScript = _enemyInstance.GetComponent<EnemyMove>();
        SetChildMoveEvent();

        return this;
    }

    public EnemyControler SetOnDeatEvent(OnEnemyDead onEnemyDead)
    {
        foreach (GameObject child in _child)
        {
            var script = child.GetComponent<EnemyDeath>();

            if (script != null)
                script.OnEnemyDeadEvent += onEnemyDead;
        }

        return this;
    }

    public EnemyControler SetEnemyStartShootBetwen(float2 startShootBetwen)
    {
        if (_enemyShootScript == null)
            return this;

        foreach (EnemyShoot script in _enemyShootScript)
        {
            script.StartShootBetwen = startShootBetwen;
            script.CalculateTimeShoot();
        }

        return this;
    }

    public EnemyControler SetEnemyTimeBetwenShoot(float timeBetwenShoot)
    {
        if (_enemyShootScript == null)
            return this;

        foreach (EnemyShoot script in _enemyShootScript)
            script.TimeBetwenShoot = timeBetwenShoot;

        return this;
    }

    public EnemyControler SetEnemyMoveSpeed(float moveSpeed)
    {
        if (_enemyMoveScript == null)
            return this;

        _enemyMoveScript.Speed = moveSpeed;

        return this;
    }

    #endregion

    public int GetCountEnemy()
    {
        return _child.Count;
    }

    public List<EnemyShoot> GetAllEnemyShoot()
    {
        return _enemyShootScript;
    }

    public bool GetStatusMoveEnemy()
    {
        return _enemyMoveToStartPosition;
    }

    public void MoveToStartPosition()
    {
        _enemyInstance.transform.position = Vector2.MoveTowards(new Vector2(_enemyInstance.transform.position.x, _enemyInstance.transform.position.y), _startPosition, 3 * Time.deltaTime);

        if (_enemyInstance.transform.position == _startPosition)
            _enemyMoveToStartPosition = false;
    }

    public void TurnMoveScript(bool status)
    {
        if(_enemyMoveScript != null)
            _enemyMoveScript.enabled = status;
    }

    public EnemyControler AddSpeedEnemy(float speed)
    {
        _enemyMoveScript.Speed += speed;
        return this;
    }

    public EnemyControler SetMoveDownSpeed(float moveDownSpeed)
    {
        _enemyMoveScript.MoveDownSpeed = moveDownSpeed;
        return this;
    }

    private void GetAllChildEnemyShoot()
    {
        _enemyShootScript = new List<EnemyShoot>();

        foreach (GameObject child in _child)
        {
            var script = child.GetComponent<EnemyShoot>();

            if (script != null && !_enemyShootScript.Contains(script))
                _enemyShootScript.Add(script);
        }
    }

    private void SetChildMoveEvent()
    {
        foreach (GameObject child in _child)
        {
            var script = child.GetComponent<EnemyColliderWall>();

            if (script != null)
                script.OnEnemyTurnEvent += _enemyMoveScript.Turn;
        }
    }

    private void GetAllChild(GameObject instanceEnemys)
    {
        _child = new List<GameObject>();

        foreach (Transform child in instanceEnemys.transform)
        {
            if (!_child.Contains(child.gameObject))
                _child.Add(child.gameObject);
        }
    }
}
