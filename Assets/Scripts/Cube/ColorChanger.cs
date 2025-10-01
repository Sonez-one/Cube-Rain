using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV(0f, 1f, 0.1f, 1f, 0.5f, 1f);
    }
}