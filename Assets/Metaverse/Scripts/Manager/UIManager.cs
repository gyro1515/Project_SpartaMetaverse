using System.Collections;
using System.Collections.Generic;
using TheStackSession;
using TMPro;
using TopDownSession;
using Unity.VisualScripting;
using UnityEngine;

namespace MetaverseSession
{
    public enum UIState
    {
        None,
        Meta,
        Flappy,
        TopDown,
        Stack,
    }
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        
        BaseUI flappyUI;
        BaseUI stackUI;
        BaseUI topDownUI;
        FinishUI finishUI;
        private static UIState currentState = UIState.None;
        public static UIState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
            }
        }

        private void Awake()
        {
            instance = this;

            // 꺼져있는 애들도 찾아오기 (true)
            flappyUI = transform.Find("FlappyUI")?.GetComponent<BaseUI>();
            flappyUI.Init(this);
            stackUI = transform.Find("StackUI")?.GetComponent<BaseUI>();
            stackUI.Init(this);
            topDownUI = transform.Find("TopDownUI")?.GetComponent<BaseUI>();
            topDownUI.Init(this);
            finishUI = transform.Find("FinishUI")?.GetComponent<FinishUI>();
            finishUI.Init(this);

            // 결과 UI 액티브
            if(currentState != UIState.None)
            {
                finishUI.gameObject.SetActive(true);
            }
            else
            {
                finishUI.gameObject.SetActive(false);
            }
            // 먼저 currentState체크 후 세팅
            ChangeState(UIState.None); 

        }

        public void ChangeState(UIState state)
        {
            currentState = state;
            flappyUI.SetActive(currentState);
            stackUI.SetActive(currentState);
            topDownUI.SetActive(currentState);
        }
        public void GetScore(out int curScore, out int bestScore)
        {
            string bestScoreK = "";
            string curScoreK = "";
            switch (currentState)
            {
                case UIState.Flappy:
                    bestScoreK = "FlappyBestScore";
                    curScoreK = "FlappyCurScore";
                    break;
                case UIState.TopDown:
                    bestScoreK = "TopDownBestScore";
                    curScoreK = "TopDownCurScore";
                    break;
                case UIState.Stack:
                    bestScoreK = "StackBestScore";
                    curScoreK = "StackCurScore";
                    break;
                default:
                    break;
            }
            curScore = PlayerPrefs.GetInt(curScoreK, 0);
            bestScore = PlayerPrefs.GetInt(bestScoreK, 0);
        }


    }
}
