using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _bombPosition;

    private void OnEnable()
    {
        _cubeSpawner.Deactivated += GetBomb;
    }

    private void OnDisable()
    {
        _cubeSpawner.Deactivated -= GetBomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.transform.position = _bombPosition;
        bomb.Renderer.material.color = Color.black;

        base.ActionOnGet(bomb);

        bomb.Releasing += Release;

        bomb.StartCountdown();
    }

    protected override void Release(Bomb bomb)
    {
        bomb.Releasing -= Release;

        base.Release(bomb);
    }

    private void GetBomb(Vector3 position)
    {
        _bombPosition = position;

        Pool.Get();
    }
}