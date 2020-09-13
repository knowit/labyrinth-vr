using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public static class BehaviourExtensions
{
    public static IslpConnection GetIslpConnection(this MonoBehaviour component)
    {
        var connection = UnityObject.FindObjectOfType<IslpConnection>();
        if (connection)
            return connection;
        return null;
    }

    public static IGameManager GetGameManager(this MonoBehaviour component)
    {
        var manager = UnityObject.FindObjectOfType<GameManager>();
        if (manager)
            return manager;

        Debug.Log("Using mock game manager");
        return new GameManagerMock();
    }
}
