using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerController player { get; private set; }
        private ResourceController _playerResourceController;

        [SerializeField] private int currentWaveIndex = 0;

        private EnemyManager enemyManager;
        private UIManager uiManager;
        public static bool isFirstLoading = true;
        private const string BestScoreKey = "TopDownBestScore";
        private const string CurScoreKey = "TopDownCurScore";

        private void Awake()
        {
            instance = this;
            player = FindObjectOfType<PlayerController>();
            player.Init(this);

            uiManager = FindObjectOfType<UIManager>();

            _playerResourceController = player.GetComponent<ResourceController>();
            _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
            _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

            enemyManager = GetComponentInChildren<EnemyManager>();
            enemyManager.Init(this);




        }
        private void Start()
        {
            StartCoroutine(FadeManager.Instance.FadeIn());
            if (!isFirstLoading)
            {
                StartGame();
            }
            else
            {
                isFirstLoading = false;
            }
        }
        public void StartGame()
        {
            uiManager.SetPlayGame();
            StartNextWave();
        }

        void StartNextWave()
        {
            currentWaveIndex += 1;
            uiManager.ChangeWave(currentWaveIndex);
            enemyManager.StartWave(1 + currentWaveIndex / 5);
        }

        public void EndOfWave()
        {
            StartNextWave();
        }

        public void GameOver()
        {
            int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            if (currentWaveIndex > bestScore)
            {
                PlayerPrefs.SetInt(BestScoreKey, currentWaveIndex);
            }
            PlayerPrefs.SetInt(CurScoreKey, currentWaveIndex);
            enemyManager.StopWave();
            uiManager.SetGameOver();

        }

    }
}