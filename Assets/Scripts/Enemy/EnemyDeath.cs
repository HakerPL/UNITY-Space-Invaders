using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyDeath : MonoBehaviour
    {
        public float TimeLiveEffectDestroy = 1f;
        public GameObject EffectPrefab;
        public int Points = 10;
        public AudioClip DeadSound;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "EnemyBullet" || other.tag != "PlayerBullet")
                return;

            DestroyShip();

            Destroy(other.gameObject);
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

            AudioSource.PlayClipAtPoint(DeadSound, gameObject.transform.position);

            gameObject.SetActive(false);

            Destroy(instance.gameObject, TimeLiveEffectDestroy);
            Destroy(gameObject);
        }
    }
}
