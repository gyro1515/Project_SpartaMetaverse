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
            StartCoroutine(NextSceneSequence());
            //SetActive(UIState.None); // ���� �� �ڵ����� ����
        }


        public void OnClickCancelButton() // ��� Ŭ�� ��
        {
            GameManager.instance.player.CanMove = true;

            SetActive(UIState.None); // UI ����
        }
        
        public void SetActive(UIState state)
        {
            //Debug.Log($"{state} / {this.state} / {gameObject.name}");
            if(gameObject == null)
            {
                Debug.Log($"���� ȣ��ǳ�");

                return;
            }
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