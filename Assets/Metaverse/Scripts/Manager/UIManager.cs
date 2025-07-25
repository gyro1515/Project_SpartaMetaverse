using System.Collections;
using System.Collections.Generic;
using TopDownSession;
using UnityEngine;

namespace MetaverseSession
{
    public enum UIState
    {
        None,
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
        private UIState currentState;

        private void Awake()
        {
            instance = this;

            // 꺼져있는 애들도 찾아오기 (true)
            flappyUI = GetComponentInChildren<BaseUI>(true);
            flappyUI.Init(this);
            stackUI = GetComponentInChildren<BaseUI>(true);
            stackUI.Init(this);
            topDownUI = GetComponentInChildren<BaseUI>(true);
            topDownUI.Init(this);

            ChangeState(UIState.None);
        }

        public void ChangeState(UIState state)
        {
            currentState = state;
            flappyUI.SetActive(currentState);
            stackUI.SetActive(currentState);
            topDownUI.SetActive(currentState);
        }
    }
}
