using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    // 페이드 인 아웃 체크용
    [HideInInspector] public bool isFadeOut = false;

    private void Awake()
    {
        // 싱글턴 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public IEnumerator FadeOut()
    {
        //Debug.Log("FadeOut");
        fadeImage.raycastTarget = true;
        isFadeOut = true;
        float time = 0f;
        Color c = fadeImage.color;
        c.a = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;

    }

    public IEnumerator FadeIn()
    {
        //Debug.Log("FadeIn");
        float time = 0f;
        Color c = fadeImage.color;
        c.a = 1f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
        fadeImage.raycastTarget = false;
        isFadeOut = false;

    }

}
