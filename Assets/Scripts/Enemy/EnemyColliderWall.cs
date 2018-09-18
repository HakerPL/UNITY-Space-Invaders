using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public enum Direction
    {
        Left = -1,
        Right = 1
    }

    public class EnemyColliderWall : MonoBehaviour
    {
        public delegate void OnEnemyTurn(Direction direction);
        public event OnEnemyTurn OnEnemyTurnEvent;

        void OnCollisionEnter2D(Collision2D col)
        {
            var wallReflect = col.gameObject.GetComponent<WallReflect>();

            if (wallReflect == null)
                return;

            OnEnemyTurnEvent?.Invoke(wallReflect.ReflectDirection);
        }
    }
}