using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager _inputs;

    [SerializeField]
    private float _movementSpeed = 5f;

    private Vector2 _movementInput;

    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();

#if UNITY_EDITOR
        if (IsInputNull())
        {
            Debug.Log("Not Found");
        }
#endif
    }

    private void OnEnable()
    {
        _inputs.onMovementUpdate += OnMovementInput;
    }

    private void OnDisable()
    {
        _inputs.onMovementUpdate -= OnMovementInput;
    }

    private void Update()
    {
        transform.Translate(_movementInput * _movementSpeed * Time.deltaTime);
    }

#region -== INPUTS ==-
    private bool IsInputNull() => _inputs == null;

    private void OnMovementInput(Vector2 axis)
    {
        if (IsInputNull()) return;
        _movementInput = axis;
    }
#endregion
}