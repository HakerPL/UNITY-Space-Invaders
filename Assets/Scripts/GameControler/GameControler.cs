using System.Collections;
using Assets.Scripts.Enemy;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GameControler
{
    [RequireComponent(typeof(GuiControler))]
    public class GameControler : MonoBehaviour
    {
        [Header("Player")]
        public GameObject PlayerPrefab;
        public Transform PlayerStartPosition;
        [Space(10)]

        [Header("PlayerLife")]
        public int PlayerLife;
        public SpawnLifes SpawnLifesObject;

        private PlayerSpawn _playerSpawn;
        [Space(10)]

        [Header("Enemy")]
        public GameObject EnemysPrefab;
        public float2 StartShootBetwen;
        public float TimeBetwenShoot;
        public float MoveSpeed;
        public float MoveDownSpeed;
        public float SpeedAddWhenDead;
        public float HowManyDeathsIncreaseSpeed;

        private int _enemyCount;
        private int _deadEnemy;

        public delegate void SetShootStatus(bool status);
        public event SetShootStatus SetShootStatusEvent;

        private EnemyControler _enemyCotroler;
        [Space(10)]

        [Header("Point")]
        public Text PointObject;
        private int _point;
        private PointControler _pointControler;
        [Space(10)]

        [Header("GameOverWall")]
        public GameObject EndGameWall;

        private GuiControler _guiControler;
        // Use this for initialization
        void Start()
        {
            _guiControler = GetComponent<GuiControler>();

            LifeSpawn();

            var wall = EndGameWall.GetComponent<GameOverWall>();
            if (wall != null)
                wall.OnEnemyEnterEvent += GameOver;

            _playerSpawn = new PlayerSpawn(PlayerPrefab, PlayerStartPosition);
            _playerSpawn.SpawnPlayer()
                .SetPlayerDeadEvent(OnPlayerDead);

            _enemyCotroler = new EnemyControler();
            _point = 0;
            _pointControler = new PointControler(PointObject);
            _pointControler.UpdatePoint(_point);

            GetPlayerShootStatus();

            StartCoroutine(StartGame());
        }

        private void GetPlayerShootStatus()
        {
            var shootScript = _playerSpawn.GetShootStatusEvent();
            if (shootScript != null)
                SetShootStatusEvent += shootScript;
        }

        private IEnumerator StartGame()
        {
            if (_guiControler != null)
                StartCoroutine(_guiControler.ShowStart());

            if (_guiControler != null)
            {
                while (_guiControler.IsShow)
                {
                    yield return 0;
                }
            }

            StartCoroutine(EnemySpawnAnimation());
        }

        private void GetAllEnemyShootStatus()
        {
            foreach (EnemyShoot shootEnemyScript in _enemyCotroler.GetAllEnemyShoot())
                SetShootStatusEvent += shootEnemyScript.SetShootStatys;
        }

        private void OnPlayerDead()
        {
            if (SetShootStatusEvent != null)
                SetShootStatusEvent -= _playerSpawn.GetShootStatusEvent();

            TurnOffScripts(false);

            SpawnLifesObject.DestroyLife();

            StartCoroutine(PlayerDead());
        }

        private IEnumerator PlayerDead()
        {
            if (_guiControler != null)
            {
                if (SpawnLifesObject.GetCountLifes() == 0)
                {
                    GameOver();
                    yield break;
                }
                else
                {
                    StartCoroutine(_guiControler.ShowDead());
                    while (_guiControler.IsShow)
                    {
                        yield return 0;
                    }
                }
            }

            _playerSpawn.SpawnPlayer()
                .SetPlayerDeadEvent(OnPlayerDead);

            SetShootStatusEvent += _playerSpawn.GetShootStatusEvent();

            TurnOffScripts(true);
        }

        private IEnumerator EnemySpawnAnimation()
        {
            _deadEnemy = 0;
            _enemyCotroler.CreateEnemys(EnemysPrefab)
                .SetOnDeatEvent(OnEnemyDead)
                .SetEnemyStartShootBetwen(StartShootBetwen)
                .SetEnemyTimeBetwenShoot(TimeBetwenShoot)
                .SetStartEnemyMoveSpeed(MoveSpeed)
                .SetMoveDownSpeed(MoveDownSpeed);

            GetAllEnemyShootStatus();
            _enemyCount = _enemyCotroler.GetCountEnemy();

            TurnOffScripts(false);

            if (_guiControler != null)
            {
                StartCoroutine(_guiControler.ShowNewWave());
                while (_guiControler.IsShow)
                {
                    yield return 0;
                }
            }

            while (_enemyCotroler.GetStatusMoveEnemy())
            {
                _enemyCotroler.MoveToStartPosition();
                yield return 0;
            }

            TurnOffScripts(true);
        }

        private void OnEnemyDead(int points)
        {
            if (_enemyCount == 0)
                return;

            _enemyCount--;

            _point += points;
            _pointControler.UpdatePoint(_point);

            _deadEnemy++;

            if (_enemyCount == 0)
                StartCoroutine(EnemySpawnAnimation());
            else if (_deadEnemy % HowManyDeathsIncreaseSpeed == 0)
                _enemyCotroler.AddSpeedEnemy(SpeedAddWhenDead);
        }

        private void LifeSpawn()
        {
            SpawnLifesObject.Spawn(PlayerLife);
        }

        private void GameOver()
        {
            TurnOffScripts(false);
            _playerSpawn.DestroyPlayer();
            StartCoroutine(GameOverCoroutine());
        }

        private IEnumerator GameOverCoroutine()
        {
            StartCoroutine(_guiControler.ShowGameOver());
            while (_guiControler.IsShow)
            {
                yield return 0;
            }

            SceneManager.LoadScene("Menu");
        }

        private void TurnOffScripts(bool starus)
        {
            _enemyCotroler.TurnMoveScript(starus);
            SetShootStatusEvent?.Invoke(starus);
        }
    }
}
