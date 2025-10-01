using System.Collections;
using UnityEngine;
using System;

public class Cube : PoolableObject
{
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
        float minLifeTime = 2f;
        float maxLifeTime = 5f;

        yield return new WaitForSeconds(UnityEngine.Random.Range(minLifeTime, maxLifeTime + 1)); ;

        Releasing?.Invoke(this);
    }
}