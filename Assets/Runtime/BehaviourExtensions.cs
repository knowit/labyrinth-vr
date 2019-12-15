using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

public static class BehaviourExtensions
{
    public static ServerConnection GetServerConnection(this MonoBehaviour component, bool createNew=false)
    {
        var connection = UnityObject.FindObjectOfType<ServerConnection>();
        if (connection)
            return connection;

        if (component.gameObject.TryGetComponent(out connection))
            return connection;

        if (!createNew)
            throw new Exception("No server connection in anywhere");

        return new GameObject("ServerConnection", typeof(ServerConnection))
            .GetComponent<ServerConnection>();
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
