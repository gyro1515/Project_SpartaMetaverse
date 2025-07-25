using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetaverseSession
{
    public abstract class BaseUI : MonoBehaviour
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
            // 게임진입
            StartCoroutine(NextSceneSequence());
        }


        public void OnClickCancelButton() // 취소 클릭 시
        {
            SetActive(UIState.None); // UI 끄기
        }
        
        public void SetActive(UIState state)
        {
            gameObject.SetActive(this.state == state);
        }
        public IEnumerator NextSceneSequence()
        {
            yield return FadeManager.Instance.FadeOut(); // 페이드 아웃 끝나고
            Debug.Log("다음 씬으로");
            SceneLoader.Load(nextScene);
        }
    }
}