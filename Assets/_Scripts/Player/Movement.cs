using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private InputManager _inputs;

    [SerializeField]
    private float _movementSpeed = 15f;

    //TODO dash
    [SerializeField]
    private float _dashSpeed = .125f;

    [SerializeField]
    private float _dashDistance = 10f;

    [SerializeField]
    private float _dashCooldown = 3f;

    private Vector2 _movementInput;
    private bool _onDashCooldown = false;
    private bool _isDashing = false;

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
        _inputs.onDash += OnDashPerformed;
    }

    private void OnDisable()
    {
        _inputs.onMovementUpdate -= OnMovementInput;
        _inputs.onDash -= OnDashPerformed;
    }

    private void Update()
    {
        
        if (!_isDashing)
        {
            transform.Translate(_movementInput * _movementSpeed * Time.deltaTime);
        }
        else 
        {
            if (!_onDashCooldown)
            {
                transform.position = Vector2.Lerp((Vector2)transform.position, (Vector2)transform.position + _movementInput * _dashDistance, _dashSpeed * Time.deltaTime);
                StartCoroutine(DashCooldown());
            }
            
            _isDashing = false;
        }

    }

#region -== INPUTS ==-
    private bool IsInputNull() => _inputs == null;

    private void OnMovementInput(Vector2 axis)
    {
        if (IsInputNull()) return;
        _movementInput = axis;
    }

    private void OnDashPerformed(float value)
    {
        if (IsInputNull()) return;
        if (value == 1) 
        {
            _isDashing = true;
        }
    }

    private IEnumerator DashCooldown()
    {
        float elapsedTime = 0f;
        _onDashCooldown = true;
        while (elapsedTime < _dashCooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _onDashCooldown = false;
    }
#endregion
}