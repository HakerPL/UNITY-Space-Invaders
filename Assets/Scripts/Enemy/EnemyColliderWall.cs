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

public class EnemyColliderWall : MonoBehaviour
{
    public delegate void OnEnemyTurn(DIRECTION direction);
    public event OnEnemyTurn OnEnemyTurnEvent;

    void OnCollisionEnter2D(Collision2D col)
    {
        var wallReflect = col.gameObject.GetComponent<WallReflect>();

        if (wallReflect == null)
            return;

        //Turn(wallReflect.ReflectDirection);
        OnEnemyTurnEvent?.Invoke(wallReflect.ReflectDirection);
    }
}
