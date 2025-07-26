using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MetaverseSession
{
    public class ShowGameUI : MonoBehaviour
    {
        [SerializeField] UIState uiState;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 6) // �÷��̾��� ����, �±׷� ����?
            {
                UIManager.instance.ChangeState(uiState);
                GameManager.instance.player.CanMove = false;

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            // �� ��ȯ�� OnTriggerExit2D�� ȣ��Ǹ鼭 �ı��� ������Ʈ�� ���� �� �� ����
            if (SceneLoader.IsChange)
            {
                Debug.Log("�ı��� UIManager ������Ʈ ȣ��");
                return; // �� ��ȯ �߿��� �����ϵ���
            }

            if (collision.gameObject.layer == 6)
            {
                UIManager.instance.ChangeState(UIState.None);
            }
        }
    }
}