using System;
using UnityEngine;

public class PointerInputReader : MonoBehaviour
{
    [SerializeField] private InteractionButton _interactionButton = InteractionButton.Primary;

    public event Action<Vector2> PointerPressed;

    private enum InteractionButton
    {
        Primary = 0,
        Secondary = 1
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)_interactionButton))
            PointerPressed?.Invoke(Input.mousePosition);
    }
}
