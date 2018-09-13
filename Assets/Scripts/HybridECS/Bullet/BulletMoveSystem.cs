using Assets.Scripts.HybridECS.BulletSpeed;
using Unity.Entities;
using UnityEngine;

public class BulletMoveSystem : ComponentSystem
{
    struct BulletMoveComponents
    {
        public BulletSpeed BulletSpeed;
        public Rigidbody2D Rigidbody2D;
        public Transform Transform;
    }

    // Will run every frame
    protected override void OnUpdate()
    {
        float delta = Time.deltaTime;

        foreach (var bulletMoveComponents in GetEntities<BulletMoveComponents>())
        {
            float force = bulletMoveComponents.BulletSpeed.Speed * delta * 50;
            bulletMoveComponents.Rigidbody2D.velocity = bulletMoveComponents.Transform.forward * force;
        }
    }
}

