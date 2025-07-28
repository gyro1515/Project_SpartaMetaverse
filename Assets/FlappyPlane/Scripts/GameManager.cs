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
        private const string BestScoreKey = "FlappyBestScore";
        private const string CurScoreKey = "FlappyCurScore";
        private static bool isStartOnce = false; // 게임 시작 여부
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
            // Flappy씬이 먼저 실행되더라도 메타 버스 UI 결과 출력되도록
            MetaverseSession.UIManager.CurrentState = MetaverseSession.UIState.Flappy; 
        }
        public void GameOver()
        {
            //Debug.Log("Game Over");
            // 현재 점수 저장, 최고 점수 갱신
            int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            if (currentScore > bestScore)
            {
                PlayerPrefs.SetInt(BestScoreKey, currentScore);
            }
            PlayerPrefs.SetInt(CurScoreKey, currentScore);
            Debug.Log($"게임오버 {currentScore} / {isStartOnce}");
            uiManager.SetRestart();
        }

        public void RestartGame()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StartCoroutine(SceneLoader.NextSceneSequence(SceneState.FlappyPlane));

        }

        public void AddScore(int score)
        {
            // 죽었다면 점수 증가 x
            if (player.isDead)
            {
                //Debug.Log("IsDie");
                return;
            }
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            //Debug.Log("Score: " + currentScore);
        }
        public void GameStart()
        {
            player.GameStart();
            UIManager.StartUI.SetActive(false); // UI 끄기
            isStartOnce = true;
        }
        public void ReturnToMetaverse()
        {
            // 게임을 진행 안하고 돌아가면 0점 출력되도록
            if (!isStartOnce)
            {
                PlayerPrefs.SetInt(CurScoreKey, currentScore);
            }
            isStartOnce = false;
            Debug.Log($"리턴 {currentScore} / {isStartOnce}");

            StartCoroutine(SceneLoader.NextSceneSequence(SceneState.Metaverse));
        }

    }
}

