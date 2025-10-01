using System.Collections;
using UnityEngine;
using System;

public class Cube : PoolableObject
{
    private readonly float _minLifeTime = 2f;
    private readonly float _maxLifeTime = 5f;

    [SerializeField] private ColorChanger _colorChanger;

    private bool _isColorChanged = false;
    
    public event Action<Cube> Releasing;

    public void Init(bool isColorChanged)
    {
        _isColorChanged = isColorChanged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _) && !_isColorChanged)
        {
            _colorChanger.ChangeColor();
            _isColorChanged = true;
            StartCoroutine(WaitForRelease());
        }
    }

    private IEnumerator WaitForRelease()
    {
        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
        var wait = new WaitForSeconds(lifeTime);

        yield return wait;

        Releasing?.Invoke(this);
    }
}