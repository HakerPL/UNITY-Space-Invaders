using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyShields : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "Enemy")
                return;

            Destroy(gameObject);
        }
    }
}
