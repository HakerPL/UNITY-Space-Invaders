using Assets.Scripts.HybridECS;
using Unity.Entities;
using UnityEngine;

public class EnemyShootSytem : ComponentSystem
{
    struct EnemyShootComponents
    {
        public EnemyShoot EnemyShoot;
        public BulletShot Bullet;
        public AudioSource SoundEffect;
    }

    // Will run every frame
    protected override void OnUpdate()
    {
        foreach (var enemyShootComponents in GetEntities<EnemyShootComponents>())
        {
            if (!enemyShootComponents.EnemyShoot.AllowShoot)
                continue;

            if (enemyShootComponents.EnemyShoot.StartShootTime < Time.time)
            {
                enemyShootComponents.Bullet.CreateInstance(enemyShootComponents.EnemyShoot.SpawnBullet);
                enemyShootComponents.EnemyShoot.StartShootTime = Time.time + enemyShootComponents.EnemyShoot.TimeBetwenShoot;
                enemyShootComponents.SoundEffect.Play();
            }
        }
    }
}
