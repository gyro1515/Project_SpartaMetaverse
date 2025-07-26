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
            if (collision.gameObject.layer == 6) // 플레이어일 때만, 태그로 변경?
            {
                UIManager.instance.ChangeState(uiState);
                GameManager.instance.player.CanMove = false;

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            // 씬 전환시 OnTriggerExit2D가 호출되면서 파괴된 오브젝트에 접근 할 수 있음
            if (SceneLoader.IsChange)
            {
                Debug.Log("파괴된 UIManager 오브젝트 호출");
                return; // 씬 전환 중에는 무시하도록
            }

            if (collision.gameObject.layer == 6)
            {
                UIManager.instance.ChangeState(UIState.None);
            }
        }
    }
}