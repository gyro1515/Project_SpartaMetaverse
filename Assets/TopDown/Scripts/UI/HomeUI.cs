using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownSession
{
    public class HomeUI : BaseUI
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }

        public void OnClickStartButton()
        {
            GameManager.instance.StartGame();
        }

        public void OnClickExitButton()
        {
            //Application.Quit();
            GameManager.isFirstLoading = true; // 돌아가면 처음부터 다시 시작
            StartCoroutine(SceneLoader.NextSceneSequence(SceneState.Metaverse));
        }

        protected override UIState GetUIState()
        {
            return UIState.Home;
        }
    }
}