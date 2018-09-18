using UnityEngine;

public class GameOverWall : MonoBehaviour
{
    public delegate void OnEnemyEnter();
    public event OnEnemyEnter OnEnemyEnterEvent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy")
            return;

        OnEnemyEnterEvent?.Invoke();
    }
}
