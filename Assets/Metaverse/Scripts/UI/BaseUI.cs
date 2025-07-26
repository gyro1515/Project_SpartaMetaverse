using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MetaverseSession
{
    public class BaseUI : MonoBehaviour
    {
        protected UIManager uiManager;

        [SerializeField] protected Button enter;
        [SerializeField] protected Button cancel;
        [SerializeField] protected UIState state;
        [SerializeField] SceneState nextScene;

        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
            enter.onClick.AddListener(OnClickEnterButton);
            cancel.onClick.AddListener(OnClickCancelButton);
        }
        public void OnClickEnterButton() // 진입 클릭 시
        {
            GameManager.instance.player.CanMove = false;
            // 게임진입
            StartCoroutine(SceneLoader.NextSceneSequence(nextScene));
            //SetActive(UIState.None); // 진입 시 자동으로 끄기
        }


        public void OnClickCancelButton() // 취소 클릭 시
        {
            GameManager.instance.player.CanMove = true;

            SetActive(UIState.None); // UI 끄기
        }
        
        public void SetActive(UIState state)
        {
            gameObject.SetActive(this.state == state);
        }
        
    }
}