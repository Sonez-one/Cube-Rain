using TMPro;
using UnityEngine;

public class CountViewer<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private Counter<T> _counter;
    [SerializeField] private TextMeshProUGUI _spawnedObjectCount;
    [SerializeField] private TextMeshProUGUI _createdObjectCount;
    [SerializeField] private TextMeshProUGUI _activeObjectCount;

    private void OnEnable()
    {
        _counter.SpawnedObjectCountChanged += SetSpawnedObjectCount;
        _counter.CreatedObjectCountChanged += SetCreatedObjectCount;
        _counter.ActiveObjectCountChanged += SetActiveObjectCount;
    }

    private void OnDisable()
    {
        _counter.SpawnedObjectCountChanged -= SetSpawnedObjectCount;
        _counter.CreatedObjectCountChanged -= SetCreatedObjectCount;
        _counter.ActiveObjectCountChanged -= SetActiveObjectCount;
    }

    private void SetSpawnedObjectCount(int value)
    {
        _spawnedObjectCount.text = $"Spawned: {value}";
    }

    private void SetCreatedObjectCount(int value)
    {
        _createdObjectCount.text = $"Created: {value}";
    }

    private void SetActiveObjectCount(int value)
    {
        _activeObjectCount.text = $"Active: {value}";
    }
}