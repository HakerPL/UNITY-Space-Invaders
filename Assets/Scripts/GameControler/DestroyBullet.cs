using UnityEngine;

namespace Assets
{
    public class DestroyBullet : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "EnemyBullet" && other.tag != "PlayerBullet")
                return;

            Destroy(other.gameObject);
        }
    }
}
