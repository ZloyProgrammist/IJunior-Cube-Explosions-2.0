using System;
using UnityEngine;

public class CubeClickReader : MonoBehaviour
{
    [SerializeField] private PointerInputReader _inputReader;
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private Camera _camera;

    public event Action<SplitableCube> CubeClicked;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        if (_camera == null)
        {
            Debug.LogError($"{nameof(CubeClickReader)}: камера не найдена!");
        }
    }

    private void OnEnable()
    {
        if (_inputReader != null)
        {
            _inputReader.PointerPressed += OnPointerPressed;
        }
        else
        {
            Debug.LogWarning($"{nameof(CubeClickReader)}: не назначен {nameof(PointerInputReader)}!");
        }
    }


    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.PointerPressed -= OnPointerPressed;
        }
    }

    private void OnPointerPressed(Vector2 screenPosition)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _cubeLayer))
        {
            if (hit.collider.TryGetComponent(out SplitableCube resource))
            {
                CubeClicked?.Invoke(resource);
            }
        }
    }
}
