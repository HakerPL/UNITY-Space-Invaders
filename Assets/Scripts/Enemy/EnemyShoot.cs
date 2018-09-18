using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyShoot : MonoBehaviour
    {
        public float2 StartShootBetwen;
        public float TimeBetwenShoot;
        public Transform SpawnBullet;
        public GameObject BulletPrefab;
        public AudioClip ShootSound;

        private float _lastShootTime;
        private bool _allowShoot;
        private AudioSource _audioSource;

        public void SetShootStatys(bool status)
        {
            _lastShootTime = SetTimeShoot() + TimeBetwenShoot;
            _allowShoot = status;
        }

        public void CalculateTimeShoot()
        {
            _lastShootTime = SetTimeShoot();
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            CalculateTimeShoot();
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!_allowShoot)
                return;

            if (_lastShootTime < Time.time)
            {
                Instantiate(BulletPrefab, SpawnBullet.position, BulletPrefab.transform.rotation);
                _lastShootTime = Time.time + TimeBetwenShoot;
                if (_audioSource != null)
                    _audioSource.PlayOneShot(ShootSound);
            }
        }

        private float SetTimeShoot()
        {
            return UnityEngine.Random.Range(StartShootBetwen.x, StartShootBetwen.y) + Time.time;
        }
    }
}
