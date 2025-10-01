using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TransparencyChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void StartChange(float time)
    {
        StartCoroutine(ChangeSmoothly(time));
    }

    private IEnumerator ChangeSmoothly(float time)
    {
        float timer = time;
        Color color = _renderer.material.color;

        while (color.a > 0)
        {
            timer -= Time.deltaTime;
            color.a = Mathf.Clamp01(timer / time);

            _renderer.material.color = color;

            yield return null;
        }
    }
}