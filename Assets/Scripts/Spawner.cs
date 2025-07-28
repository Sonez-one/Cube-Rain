using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Cube> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(WaitForNewCube());
    }

    private void ActionOnGet(Cube cube)
    {
        float minSpawnPositonX = -9;
        float maxSpawnPositonX = 9;
        float SpawnPositonY = 20;
        float minSpawnPositonZ = -13;
        float maxSpawnPositonZ = 13;

        Quaternion randomRotation = Random.rotation;

        cube.transform.SetPositionAndRotation(new Vector3(Random.Range(minSpawnPositonX, maxSpawnPositonX), SpawnPositonY, 
                                                          Random.Range(minSpawnPositonZ, maxSpawnPositonZ)), randomRotation);
        cube.Init(cube.GetComponent<Rigidbody>(), cube.GetComponent<Renderer>(), false);
        cube.Renderer.material.color = Color.white;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
        cube.Releasing += Release;
    }

    private void Release(Cube cube)
    {
        cube.Releasing -= Release;
        _cubes.Release(cube);
    }

    private void GetCube()
    {
        _cubes.Get();
    }

    private IEnumerator WaitForNewCube()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_repeatRate);

            GetCube();
        }
    }
}