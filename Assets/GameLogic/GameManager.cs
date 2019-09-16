using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerSpawner Spawner;
    public Transform OverviewPosition;
    public CameraMover Camera;
    public Canvas UIObject;
    public GameObject WorldObject;

    void Start()
    {
        Camera.Move(OverviewPosition.position);
    }

    public void StartGame()
    {
        UIObject.gameObject.SetActive(false);

        Spawner.SpawnPlayer();
    }

    public void RestartGame()
    {
        Camera.MoveAndRotate(OverviewPosition.position, OverviewPosition.rotation);

        Spawner.DeSpawnPlayer();
        UIObject.gameObject.SetActive(true);
    }
}
