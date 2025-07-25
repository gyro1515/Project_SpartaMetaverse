using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyPlaneSession
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gameManager;

        public static GameManager Instance
        {
            get { return gameManager; }
        }

        private int currentScore = 0;
        FlappyPlaneSession.UIManager uiManager;
        Player player;
        public FlappyPlaneSession.UIManager UIManager
        {
            get { return uiManager; }
        }

        private void Awake()
        {
            gameManager = this;
            uiManager = FindObjectOfType<FlappyPlaneSession.UIManager>();
            player = FindObjectOfType<Player>();
        }
        private void Start()
        {
            StartCoroutine(FadeManager.Instance.FadeIn());
            uiManager.UpdateScore(0);
        }
        public void GameOver()
        {
            Debug.Log("Game Over");
            uiManager.SetRestart();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddScore(int score)
        {
            // 죽었다면 점수 증가 x
            if (player.isDead)
            {
                Debug.Log("IsDie");
                return;
            }
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            Debug.Log("Score: " + currentScore);
        }
    }
}

