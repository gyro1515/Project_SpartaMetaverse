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
        public void OnClickEnterButton() // ���� Ŭ�� ��
        {
            GameManager.instance.player.CanMove = false;
            // ��������
            StartCoroutine(SceneLoader.NextSceneSequence(nextScene));
            //SetActive(UIState.None); // ���� �� �ڵ����� ����
        }


        public void OnClickCancelButton() // ��� Ŭ�� ��
        {
            GameManager.instance.player.CanMove = true;

            SetActive(UIState.None); // UI ����
        }
        
        public void SetActive(UIState state)
        {
            gameObject.SetActive(this.state == state);
        }
        
    }
}