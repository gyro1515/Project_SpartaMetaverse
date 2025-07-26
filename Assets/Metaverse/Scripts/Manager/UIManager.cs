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
        private UIState currentState;

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

            ChangeState(UIState.None);
        }

        public void ChangeState(UIState state)
        {

            currentState = state;
            // 씬 전환시 OnTriggerExit2D가 호출되면서 파괴된 오브젝트에 접근 할 수 있음
            if (flappyUI.gameObject.IsDestroyed() || stackUI.gameObject.IsDestroyed() || topDownUI.gameObject.IsDestroyed())
            {
                Debug.Log("파괴된 UI 오브젝트 호출");
                return;
            }
            flappyUI.SetActive(currentState);
            stackUI.SetActive(currentState);
            topDownUI.SetActive(currentState);

        }
        

    }
}
