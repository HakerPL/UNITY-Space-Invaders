using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.HybridECS.Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Transform))]
    public class BulletMove : MonoBehaviour
    {
        public float Speed;

        private void Start()
        {
            var rigidbody2D = GetComponent<Rigidbody2D>();
            var transform = GetComponent<Transform>();
            float force = Speed * Time.deltaTime * 50;
            rigidbody2D.velocity = transform.up * force;
        }
    }
}
