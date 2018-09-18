using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerShoot : MonoBehaviour
    {
        public float TimeBetwenShoot;
        public Transform SpawnBullet;
        public GameObject BulletPrefab;
        public AudioClip ShootSound;

        private bool _allowShoot;
        private float _lastShootTime;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
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
                if(_audioSource != null)
                    _audioSource.PlayOneShot(ShootSound);
            }
        }

        public void SetShootStatus(bool status)
        {
            _allowShoot = status;
        }
    }
}
