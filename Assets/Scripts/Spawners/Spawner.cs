using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    public event Action Created;
    public event Action Spawned;
    public event Action<Vector3> Deactivated;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.Rigidbody.velocity = Vector3.zero;
        obj.gameObject.SetActive(true);

        Spawned?.Invoke();
    }

    protected virtual void Release(T obj)
    {
        Pool.Release(obj);

        Deactivated?.Invoke(obj.transform.position);
    }

    private T Create()
    {
        T obj = Instantiate(_prefab);

        Created?.Invoke();

        return obj;
    }
}