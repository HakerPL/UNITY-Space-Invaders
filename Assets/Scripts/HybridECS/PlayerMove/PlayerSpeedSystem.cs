using Unity.Entities;
using UnityEngine;

public class PlayerSpeedSystem : ComponentSystem
{
    struct PlayerMoveComponents
    {
        public PlayerSpeed PlayerSpeed;
        public Rigidbody2D Rigidbody2D;
    }

    // Will run every frame
    protected override void OnUpdate()
    {
        float delta = Time.deltaTime;

        foreach (var playerMoveComponents in GetEntities<PlayerMoveComponents>())
        {
            float force = Input.GetAxis("Horizontal") * playerMoveComponents.PlayerSpeed.Speed * delta * 50;
            playerMoveComponents.Rigidbody2D.velocity = Vector2.right * force;
        }
    }
}
