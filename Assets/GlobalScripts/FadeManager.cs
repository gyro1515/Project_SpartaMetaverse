using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���̵� �� �ƿ��� �����ϴ� �Ŵ���, ���� ���� �� �ڵ����� ����, �� ��ȯ �� ���̵� ȿ�� ����
public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private Image fadeImage;
    private const float fadeDuration = 0.3f; // ���̵� �ð�

    // ���̵� �� �ƿ� üũ��
    [HideInInspector] public bool isFadeOut = false;

    // �ڵ� �ʱ�ȭ
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("FadeManager");
            // SceneLoader�� ��������� �ٸ� ���
            // FadeManager ������Ʈ�� �߰��Ͽ� �� ���� ���� Instance�� ����
            // ���� Instance�� ���� ��, Awake()�� ȣ�� ��
            Instance = go.AddComponent<FadeManager>();
            DontDestroyOnLoad(go);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (fadeImage == null)
            SetupFadeImage();
    }
    private void SetupFadeImage()
    {
        // ĵ���� ����
        GameObject canvasGO = new GameObject("FadeCanvas");
        canvasGO.transform.SetParent(this.transform); // FadeManager�� �ڽ����� ����
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // UI �� ���� ǥ��

        // �̹��� ����
        GameObject imgGO = new GameObject("FadeImage");
        imgGO.transform.SetParent(canvasGO.transform);
        fadeImage = imgGO.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.raycastTarget = true; // Ŭ�� �̺�Ʈ�� �޵��� �����Ͽ�, 
                                        // ���̵� �ƿ� �߿��� �ٸ� UI ��ȣ�ۿ��� ����

        // Ǯ��ũ�� ����
        RectTransform rt = fadeImage.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    // �Ʒ� �Լ��鵵 static���� ���� �����ϳ�, �� instance.�� �ٿ� ����ؾ� ��
    // ���� �ν��Ͻ� �޼ҵ�� ���� -> FadeManager.Instance.FadeOut()���� ���
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
