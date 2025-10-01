using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 1f;

    private void Start()
    {
        StartCoroutine(WaitForNewCube());
    }

    protected override void ActionOnGet(Cube cube)
    {
        float minSpawnPositonX = -9f;
        float maxSpawnPositonX = 9f;
        float SpawnPositonY = 20f;
        float minSpawnPositonZ = -13f;
        float maxSpawnPositonZ = 13f;

        Quaternion randomRotation = Random.rotation;

        cube.transform.SetPositionAndRotation(new Vector3(Random.Range(minSpawnPositonX, maxSpawnPositonX), SpawnPositonY,
                                                          Random.Range(minSpawnPositonZ, maxSpawnPositonZ)), randomRotation);
        cube.Init(false);
        cube.Renderer.material.color = Color.white;

        base.ActionOnGet(cube);

        cube.Releasing += Release;
    }

    protected override void Release(Cube cube)
    {
        cube.Releasing -= Release;

        base.Release(cube);
    }

    private void GetCube()
    {
        Pool.Get();
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