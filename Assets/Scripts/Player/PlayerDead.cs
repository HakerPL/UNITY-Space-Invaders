using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public delegate void OnPlayerDead();
    public event OnPlayerDead OnPlayerDeadEvent;

    void OnDestroy()
    {
        OnPlayerDeadEvent();
    }
}
