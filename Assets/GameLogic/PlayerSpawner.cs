using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private GameObject _playerInstance;

    public void SpawnPlayer()
    {
        _playerInstance = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }

    public void DeSpawnPlayer()
    {
        Destroy(_playerInstance);
    }
}
