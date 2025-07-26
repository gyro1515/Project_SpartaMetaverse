using FlappyPlaneSession;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    None,
    Metaverse,
    TopDown,
    FlappyPlane,
    TheStack
}
// �� ��ȯ�� �����ϴ� ��ũ��Ʈ, ���� ���� �� �ڵ����� �����Ǹ�, �� ��ȯ�� ���
public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    // ���⿡ ���� �ѹ��� �� �߰�
    private readonly Dictionary<SceneState, string> sceneNames = new()
    {
        { SceneState.Metaverse,   "MetaverseScene" },
        { SceneState.TopDown,     "TopDownScene" },
        { SceneState.FlappyPlane, "FlappyPlaneScene" },
        { SceneState.TheStack,    "TheStackScene" }
    };
    public static bool IsChange { get; private set; } = false; // �� ��ȯ �� �� �� ��ȣ�ۿ� �۵� ���ϵ���

    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ���̾��Ű â�� ���ӿ�����Ʈ�� ������ �ʾƵ� �ڵ� ����
    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("SceneLoader");
            // FadeManager�� �ٸ� ���� ���, �� ��� �� ���� �ʿ�
            go.AddComponent<SceneLoader>(); // ������Ʈ �߰� �� Awake()�� ȣ��Ǿ� instance�� ������
        }
    }
    private void Awake()
    {
        if (instance == null) // ó�� AddComponent�� ȣ�� �� ���� null, 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // BeforeSceneLoad������ ���� �����ϳ� ���������� Awake()���� ����
            // ���� OnSceneLoaded�� static�� �ƴϹǷ�, ���� �����δ� Initialize()���� ���� �Ұ�
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }
    public static void Load(SceneState state)
    {
        IsChange = true; // �� ��ȯ ����
        SceneManager.LoadScene(instance.sceneNames[state]);
    }

    public static string GetSceneName(SceneState state)
    {
        return instance.sceneNames.TryGetValue(state, out var name) ? name : null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        //Debug.Log("�� ��ȯ �Ϸ�");
        IsChange = false;
    }
    public static IEnumerator NextSceneSequence(SceneState nextScene)
    {
        yield return FadeManager.Instance.FadeOut(); // ���̵� �ƿ� ������
        Debug.Log("���� ������");
        SceneLoader.Load(nextScene);
    }
}