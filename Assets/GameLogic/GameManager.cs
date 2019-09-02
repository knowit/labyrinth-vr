using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerSpawner Spawner;
    public Camera OverviewCamera;
    public Canvas UIObject;
    public GameObject WorldObject;

    public void StartGame()
    {
        OverviewCamera.gameObject.SetActive(false);
        UIObject.gameObject.SetActive(false);

        Spawner.SpawnPlayer();
        //WorldObject.AddComponent<WorldTilt>();
    }

    public void RestartGame()
    {
        Spawner.DeSpawnPlayer();
        //Destroy(WorldObject.GetComponent<WorldTilt>());

        OverviewCamera.gameObject.SetActive(true);
        UIObject.gameObject.SetActive(true);
    }
}
