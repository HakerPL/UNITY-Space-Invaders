﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public float TimeLiveEffectDestroy = 1f;
    public GameObject EffectPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet" || other.tag != "PlayerBullet")
            return;

        Destroy(other.gameObject);
        DestroyShip();
    }

    void DestroyShip()
    {
        var instance = Instantiate(EffectPrefab, transform.position, transform.rotation);
        Destroy(instance.gameObject, TimeLiveEffectDestroy);
        Destroy(gameObject);
    }
}
