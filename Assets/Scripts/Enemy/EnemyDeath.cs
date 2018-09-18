using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        public float TimeLiveEffectDestroy = 1f;
        public GameObject EffectPrefab;
        public int Points = 10;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "EnemyBullet" || other.tag != "PlayerBullet")
                return;

            Destroy(other.gameObject);
            DestroyShip();
        }

        public delegate void OnEnemyDead(int points);
        public event OnEnemyDead OnEnemyDeadEvent;

        private void OnDestroy()
        {
            OnEnemyDeadEvent?.Invoke(Points);
        }

        private void DestroyShip()
        {
            var instance = Instantiate(EffectPrefab, transform.position, transform.rotation);
            Destroy(instance.gameObject, TimeLiveEffectDestroy);
            Destroy(gameObject);
        }
    }
}
