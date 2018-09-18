using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerDead : MonoBehaviour
    {
        public AudioClip DeadSound;
        public float TimeLiveEffectDestroy = 1f;
        public GameObject EffectPrefab;

        public delegate void OnPlayerDead();
        public event OnPlayerDead OnPlayerDeadEvent;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "EnemyBullet")
                return;

            DestroyShip();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }


        private void DestroyShip()
        {
            var instance = Instantiate(EffectPrefab, transform.position, transform.rotation);

            AudioSource.PlayClipAtPoint(DeadSound, gameObject.transform.position);

            Destroy(instance.gameObject, TimeLiveEffectDestroy);

            OnPlayerDeadEvent?.Invoke();
        }
    }
}
