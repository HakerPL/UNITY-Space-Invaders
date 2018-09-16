using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "EnemyBullet")
            return;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
