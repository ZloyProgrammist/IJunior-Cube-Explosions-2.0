using UnityEngine;

public class RandomColorProvider : MonoBehaviour
{
    [Header("Настройки цвета (HSV)")]
    [SerializeField, Range(0f, 1f)] private float _hueMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _hueMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _saturationMin = 0.6f;
    [SerializeField, Range(0f, 1f)] private float _saturationMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _valueMin = 0.6f;
    [SerializeField, Range(0f, 1f)] private float _valueMax = 1f;

    public Color Generate()
    {
        return Random.ColorHSV(
            _hueMin, _hueMax,
            _saturationMin, _saturationMax,
            _valueMin, _valueMax);
    }
}