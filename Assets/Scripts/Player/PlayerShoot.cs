using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float TimeBetwenShoot;
    public Transform SpawnBullet;
    public GameObject BulletPrefab;

    private bool _allowShoot;
    private float _lastShootTime;

    private void Start()
    {
        _lastShootTime = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (!_allowShoot)
            return;

        if (Input.GetKeyDown("space") && _lastShootTime < Time.time)
        {
            Instantiate(BulletPrefab, SpawnBullet.position, BulletPrefab.transform.rotation);
            _lastShootTime = Time.time + TimeBetwenShoot;
        }
    }

    public void SetShootStatus(bool status)
    {
        _allowShoot = status;
    }
}
