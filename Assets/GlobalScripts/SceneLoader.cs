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

    // 여기에 씬들 한번에 다 추가
    private static readonly Dictionary<SceneState, string> sceneNames = new()
    {
        { SceneState.Metaverse,   "MetaverseScene" },
        { SceneState.TopDown,     "TopDownScene" },
        { SceneState.FlappyPlane, "FlappyPlaneScene" },
        { SceneState.TheStack,    "TheStackScene" }
    };
    public static bool IsChange { get; private set; } = false; // 씬 전환 시 그 후 상호작용 작동 안하도록

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // 하이어아키 창에 게임오브젝트를 만들지 않아도 자동 생성
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
            Destroy(gameObject); // 중복 방지
        }
    }
    public static void Load(SceneState state)
    {
        IsChange = true; // 씬 전환 시작
        SceneManager.LoadScene(sceneNames[state]);
    }

    public static string GetSceneName(SceneState state)
    {
        return sceneNames.TryGetValue(state, out var name) ? name : null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        Debug.Log("씬 전환 완료");
        IsChange = false;
    }
}