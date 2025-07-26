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
public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    // ���⿡ ���� �ѹ��� �� �߰�
    private static readonly Dictionary<SceneState, string> sceneNames = new()
    {
        { SceneState.Metaverse,   "MetaverseScene" },
        { SceneState.TopDown,     "TopDownScene" },
        { SceneState.FlappyPlane, "FlappyPlaneScene" },
        { SceneState.TheStack,    "TheStackScene" }
    };
    public static bool IsChange { get; private set; } = false; // �� ��ȯ �� �� �� ��ȣ�ۿ� �۵� ���ϵ���

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // ���̾��Ű â�� ���ӿ�����Ʈ�� ������ �ʾƵ� �ڵ� ����
    private static void Initialize()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("SceneLoader");
            instance = go.AddComponent<SceneLoader>();
            DontDestroyOnLoad(go);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        SceneManager.LoadScene(sceneNames[state]);
    }

    public static string GetSceneName(SceneState state)
    {
        return sceneNames.TryGetValue(state, out var name) ? name : null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        Debug.Log("�� ��ȯ �Ϸ�");
        IsChange = false;
    }
}