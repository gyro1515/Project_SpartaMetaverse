using System.Collections;
using System.Collections.Generic;
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
            if (collision.gameObject.layer == 6)
            {
                UIManager.instance.ChangeState(UIState.None);
            }
        }
    }
}