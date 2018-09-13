using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.HybridECS.PlayerShoot
{
    public class PlayerShootSystem : ComponentSystem
    {
        struct PlayerShootComponents
        {
            public PlayerShoot PlayerShoot;
            public BulletShot Bullet;
            public AudioSource SoundEffect;
        }

        // Will run every frame
        protected override void OnUpdate()
        {
            foreach (var playerShootComponents in GetEntities<PlayerShootComponents>())
            {
                if (!playerShootComponents.PlayerShoot.AllowShoot)
                    continue;

                if (Input.GetKeyDown("space") && playerShootComponents.PlayerShoot.StartShootTime < Time.time)
                {
                    playerShootComponents.Bullet.CreateInstance(playerShootComponents.PlayerShoot.SpawnBullet);
                    playerShootComponents.PlayerShoot.StartShootTime = Time.time + playerShootComponents.PlayerShoot.TimeBetwenShoot;
                    playerShootComponents.SoundEffect.Play();
                }
            }
        }
    }
}
