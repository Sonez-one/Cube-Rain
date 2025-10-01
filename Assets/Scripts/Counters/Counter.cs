using System;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private Spawner<T> _spawner;

    private int _spawnedObjectCount = 0;
    private int _createdObjectCount = 0;
    private int _activeObjectCount = 0;

    public event Action<int> SpawnedObjectCountChanged;
    public event Action<int> CreatedObjectCountChanged;
    public event Action<int> ActiveObjectCountChanged;

    private void OnEnable()
    {
        _spawner.Spawned += UpdateSpawnedObjectCount;
        _spawner.Created += UpdateCreatedObjectCount;
        _spawner.Deactivated += UpdateActiveObjectCount;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= UpdateSpawnedObjectCount;
        _spawner.Created -= UpdateCreatedObjectCount;
        _spawner.Deactivated -= UpdateActiveObjectCount;
    }

    private void UpdateSpawnedObjectCount()
    {
        _spawnedObjectCount++;

        SpawnedObjectCountChanged?.Invoke(_spawnedObjectCount);

        _activeObjectCount++;

        ActiveObjectCountChanged?.Invoke(_activeObjectCount);
    }

    private void UpdateCreatedObjectCount()
    {
        _createdObjectCount++;

        CreatedObjectCountChanged?.Invoke(_createdObjectCount);
    }

    private void UpdateActiveObjectCount(Vector3 positon)
    {
        _activeObjectCount--;

        ActiveObjectCountChanged?.Invoke(_activeObjectCount);
    }
}