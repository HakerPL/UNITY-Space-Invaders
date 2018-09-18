using Assets.Scripts.Player;
using UnityEngine;
using static Assets.Scripts.GameControler.GameControler;

namespace Assets.Scripts.GameControler
{
    public class PlayerSpawn
    {
        private readonly GameObject _playerPrefab;
        private readonly Transform _playerStartPosition;

        private GameObject _playerInstance;

        public PlayerSpawn(GameObject playerPrefab, Transform playerStartPosition)
        {
            _playerPrefab = playerPrefab;
            _playerStartPosition = playerStartPosition;
        }

        public void DestroyPlayer()
        {
            Object.Destroy(_playerInstance);
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
            _playerInstance = Object.Instantiate(_playerPrefab, _playerStartPosition.position, _playerStartPosition.rotation);

            return this;
        }
    }
}
