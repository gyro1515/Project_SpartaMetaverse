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
    // ���⿡ ���� �ѹ��� �� �߰�
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