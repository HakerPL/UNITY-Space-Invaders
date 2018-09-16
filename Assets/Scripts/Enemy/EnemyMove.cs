using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using System;

public enum DIRECTION
{
    Left = -1,
    Right = 1
}

[RequireComponent(typeof(Rigidbody2D), typeof(Transform))]
public class EnemyMove : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;

    public delegate void OnEnemyTurn(DIRECTION direction);
    public event OnEnemyTurn OnEnemyTurnEvent;

    // Use this for initialization
    void Start ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        Turn(DIRECTION.Right);

        AddTurnEventToAll();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var wallReflect = col.gameObject.GetComponent<WallReflect>();

        if (wallReflect == null)
            return;

        Turn(wallReflect.ReflectDirection);
        OnEnemyTurnEvent?.Invoke(wallReflect.ReflectDirection);
    }

    private void OnDestroy()
    {
        RemoveTurnEventFromAll();
    }

    private void Turn(DIRECTION direction)
    {
        float2 velocity = _rigidbody2D.velocity;
        float force = Speed * Time.deltaTime * 50;
        velocity.x = (int)direction * force;
        _rigidbody2D.velocity = velocity;
    }

    private void AddTurnEventToAll()
    {
        List<EnemyMove> enemyMoves = GameObject.FindObjectsOfType<EnemyMove>().ToList();

        foreach (EnemyMove enemyMove in enemyMoves)
        {
            if (enemyMove != this)
                OnEnemyTurnEvent += enemyMove.Turn;
        }
    }

    private void RemoveTurnEventFromAll()
    {
        List<EnemyMove> enemyMoves = GameObject.FindObjectsOfType<EnemyMove>().ToList();

        foreach (EnemyMove enemyMove in enemyMoves)
        {
            if (enemyMove != this)
                enemyMove.OnEnemyTurnEvent -= Turn;
        }
    }
}
