using UnityEngine;

namespace Assets.Scripts.GameControler
{
    public class PlayerSpawn
    {
        private GameObject _playerPrefab;
        private Transform _playerStartPosition;

        public PlayerSpawn(GameObject PlayerPrefab, Transform PlayerStartPosition)
        {
            _playerPrefab = PlayerPrefab;
            _playerStartPosition = PlayerStartPosition;
        }

        public void SpawnPlayer(PlayerDead.OnPlayerDead onPlayerDead)
        {
            var instance = MonoBehaviour.Instantiate(_playerPrefab, _playerStartPosition.position, _playerStartPosition.rotation);
            var componentPlayerDead = instance.GetComponent<PlayerDead>();

            if (componentPlayerDead == null)
                return;

            componentPlayerDead.OnPlayerDeadEvent += onPlayerDead;
        }
    }
}
