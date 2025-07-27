using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace TheStackSession
{
    public enum UIState
    {
        Home,
        Game,
        Score,
    }

    public class UIManager : MonoBehaviour
    {
        static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                return instance;
            }
        }
        UIState currentState = UIState.Home;

        HomeUI homeUI = null;

        GameUI gameUI = null;

        ScoreUI scoreUI = null;

        TheStack theStack = null;
        private void Awake()
        {
            instance = this;
            theStack = FindObjectOfType<TheStack>();

            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);
            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);
            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);

            ChangeState(UIState.Home);
        }


        public void ChangeState(UIState state)
        {
            currentState = state;
            homeUI?.SetActive(currentState);
            gameUI?.SetActive(currentState);
            scoreUI?.SetActive(currentState);
        }

        public void OnClickStart()
        {
            theStack.Restart();
            ChangeState(UIState.Game);
        }

        public void OnClickExit()
        {
            // 전처리기 세팅
#if UNITY_EDITOR // 유니티 에디터 상태라면
            //UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터 플레이 끄기
            //theStack.UpdateScore(); // 한 번은 게임을 시작하게 하기
            StartCoroutine(SceneLoader.NextSceneSequence(SceneState.Metaverse));

#else
        Application.Quit(); // 어플리케이션 종료
#endif
        }

        public void UpdateScore()
        {
            gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
        }

        public void SetScoreUI()
        {
            scoreUI.SetUI(theStack.Score, theStack.MaxCombo, theStack.BestScore, theStack.BestCombo);

            ChangeState(UIState.Score);
        }

    }
}

