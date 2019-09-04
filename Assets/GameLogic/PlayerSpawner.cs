
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private GameObject _playerInstance;

    public Rigidbody Ball => _playerInstance?.GetComponent<Rigidbody>();

    public void SpawnPlayer()
    {
        _playerInstance = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
    }

    public void DeSpawnPlayer()
    {
        Destroy(_playerInstance);
    }
}
