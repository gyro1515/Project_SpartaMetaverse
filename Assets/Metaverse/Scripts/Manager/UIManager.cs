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

            // �����ִ� �ֵ鵵 ã�ƿ��� (true)
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
            // �� ��ȯ�� OnTriggerExit2D�� ȣ��Ǹ鼭 �ı��� ������Ʈ�� ���� �� �� ����
            if (flappyUI.gameObject.IsDestroyed() || stackUI.gameObject.IsDestroyed() || topDownUI.gameObject.IsDestroyed())
            {
                Debug.Log("�ı��� UI ������Ʈ ȣ��");
                return;
            }
            flappyUI.SetActive(currentState);
            stackUI.SetActive(currentState);
            topDownUI.SetActive(currentState);

        }
        

    }
}
