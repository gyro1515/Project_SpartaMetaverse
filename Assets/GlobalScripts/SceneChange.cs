using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetaverseSession
{
    public class SceneChange : MonoBehaviour
    {
        [SerializeField] SceneState nextScene;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 6) // �÷��̾��� ����, �±׷� ����?
            {
                // ���⼭�� UI�� �۵��ϱ�

                PlayerController tmpPC = collision.GetComponent<PlayerController>();
                tmpPC.CanMove = false;
                StartCoroutine(NextSceneSequence());
            }
        }
        public IEnumerator NextSceneSequence()
        {
            yield return FadeManager.Instance.FadeOut();
            Debug.Log("���� ������");
            SceneLoader.Load(nextScene);
        }
    }
}