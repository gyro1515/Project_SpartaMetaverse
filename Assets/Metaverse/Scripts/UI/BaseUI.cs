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
        public void OnClickEnterButton() // ���� Ŭ�� ��
        {
            // ��������
            StartCoroutine(NextSceneSequence());
        }


        public void OnClickCancelButton() // ��� Ŭ�� ��
        {
            SetActive(UIState.None); // UI ����
        }
        
        public void SetActive(UIState state)
        {
            gameObject.SetActive(this.state == state);
        }
        public IEnumerator NextSceneSequence()
        {
            yield return FadeManager.Instance.FadeOut(); // ���̵� �ƿ� ������
            Debug.Log("���� ������");
            SceneLoader.Load(nextScene);
        }
    }
}