using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TopDownSession
{
    public class GameOverUI : BaseUI
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            restartButton.onClick.AddListener(OnClickRestartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void OnClickRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnClickExitButton()
        {
            GameManager.isFirstLoading = true; // 돌아가면 처음부터 다시 시작
            StartCoroutine(SceneLoader.NextSceneSequence(SceneState.Metaverse));
        }

        protected override UIState GetUIState()
        {
            return UIState.GameOver;
        }
    }
}