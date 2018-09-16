using UnityEngine;
using static GameControler;

namespace Assets.Scripts.GameControler
{
    public class PlayerSpawn
    {
        private GameObject _playerPrefab;
        private Transform _playerStartPosition;

        private GameObject _playerInstance;

        public PlayerSpawn(GameObject PlayerPrefab, Transform PlayerStartPosition)
        {
            _playerPrefab = PlayerPrefab;
            _playerStartPosition = PlayerStartPosition;
        }

        public PlayerSpawn SetPlayerDeadEvent(PlayerDead.OnPlayerDead onPlayerDead)
        {
            var componentPlayerDead = _playerInstance.GetComponent<PlayerDead>();

            if (componentPlayerDead == null)
                return this;

            componentPlayerDead.OnPlayerDeadEvent += onPlayerDead;

            return this;
        }

        public SetShootStatus GetShootStatusEvent()
        {
            var componentPlayerShoot = _playerInstance.GetComponent<PlayerShoot>();

            if (componentPlayerShoot == null)
                return null;

            return componentPlayerShoot.SetShootStatus;
        }

        public PlayerSpawn SpawnPlayer()
        {
            _playerInstance = MonoBehaviour.Instantiate(_playerPrefab, _playerStartPosition.position, _playerStartPosition.rotation);

            return this;
        }
    }
}
