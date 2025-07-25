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
public static class SceneLoader
{
    // 여기에 씬들 한번에 다 추가
    private static readonly Dictionary<SceneState, string> sceneNames = new()
    {
        { SceneState.Metaverse,   "MetaverseScene" },
        { SceneState.TopDown,     "TopDownScene" },
        { SceneState.FlappyPlane, "FlappyPlaneScene" },
        { SceneState.TheStack,    "TheStackScene" }
    };

    public static void Load(SceneState state)
    {
        SceneManager.LoadScene(sceneNames[state]);
    }

    public static string GetSceneName(SceneState state)
    {
        return sceneNames.TryGetValue(state, out var name) ? name : null;
    }
}