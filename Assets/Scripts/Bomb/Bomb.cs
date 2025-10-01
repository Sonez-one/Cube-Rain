using System;
using System.Collections;
using UnityEngine;

public class Bomb : PoolableObject
{
    private readonly float _minTimerValue = 2f;
    private readonly float _maxTimerValue = 5f;

    [SerializeField] TransparencyChanger _transparencyChanger;
    [SerializeField] Exploder _exploder;

    public event Action<Bomb> Releasing;

    public void StartCountdown()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        float timer = UnityEngine.Random.Range(_minTimerValue, _maxTimerValue);
        var wait = new WaitForSeconds(timer);

        _transparencyChanger.StartChange(timer);

        yield return wait;

        _exploder.Explode();
        Releasing?.Invoke(this);
    }
}