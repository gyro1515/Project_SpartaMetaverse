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
            if(collision.gameObject.layer == 6) // 플레이어일 때만, 태그로 변경?
            {
                // 여기서는 UI만 작동하기

                PlayerController tmpPC = collision.GetComponent<PlayerController>();
                tmpPC.CanMove = false;
                StartCoroutine(NextSceneSequence());
            }
        }
        public IEnumerator NextSceneSequence()
        {
            yield return FadeManager.Instance.FadeOut();
            Debug.Log("다음 씬으로");
            SceneLoader.Load(nextScene);
        }
    }
}