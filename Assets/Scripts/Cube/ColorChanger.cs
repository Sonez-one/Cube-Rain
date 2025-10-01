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
        float hueMin = 0f;
        float hueMax = 1f;
        float saturationMin = 0.1f;
        float saturationMax = 1f;
        float lightnessMin = 0.5f;
        float lightnessMax = 1f;

        _renderer.material.color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, lightnessMin, lightnessMax);
    }
}