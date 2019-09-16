
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private GameObject _playerInstance;

    public Rigidbody Ball => _playerInstance?.GetComponentInChildren<Rigidbody>();

    public void SpawnPlayer()
    {
        _playerInstance = Instantiate(PlayerPrefab, transform.position, Quaternion.identity);

        var playerController = FindObjectOfType<CameraMover>();

        playerController.MoveAndRotate(transform.position, transform.rotation);
    }

    public void DeSpawnPlayer()
    {
        Destroy(_playerInstance);
    }
}
