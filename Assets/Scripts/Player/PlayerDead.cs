using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerDead : MonoBehaviour
    {
        public delegate void OnPlayerDead();
        public event OnPlayerDead OnPlayerDeadEvent;

        void OnDestroy()
        {
            OnPlayerDeadEvent?.Invoke();
        }
    }
}
