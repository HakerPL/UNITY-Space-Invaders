using Unity.Mathematics;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool AllowShoot;
    public float2 StartShootBetwen;
    public float TimeBetwenShoot;
    public Transform SpawnBullet;
    public GameObject BulletPrefab;

    private float _startShoot;

    private void Start()
    {
        _startShoot = UnityEngine.Random.Range(StartShootBetwen.x, StartShootBetwen.y);
    }

    private void Update()
    {
        if (!AllowShoot)
            return;

        if (_startShoot < Time.time)
        {
            CreateInstance(SpawnBullet);
            _startShoot = Time.time + TimeBetwenShoot;
            //enemyShootComponents.SoundEffect.Play();
        }
    }

    private void CreateInstance(Transform transform)
    {
        Instantiate(BulletPrefab, transform.position, transform.rotation);
    }
}
