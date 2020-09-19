using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public static class BehaviourExtensions
{
    private static T GetSceneBehaviour<T>(string error) where T : UnityObject
    {
        var res = UnityObject.FindObjectOfType<T>();
        if (res == null) Debug.LogError(error);
        return res;
    }

    public static IslpConnection GetIslpConnection(this MonoBehaviour _)
        => GetSceneBehaviour<IslpConnectionDirect>("ISLP connection missing");

    public static GameManager GetGameManager(this MonoBehaviour _)
        => GetSceneBehaviour<GameManager>("No GameManager");

}
