using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputType {
    Player,
    UI
}

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputType _initialInputType;
    private Inputs _inputs;
    public event Action<Vector2> onMovementUpdate;
    public event Action<Vector2> onMousePositionUpdate;
    public event Action<float> onMouseClicked;
    public event Action<float> onMouseScrolled;
    public event Action<float> onDash;
    public event Action<float> onPrimary;
    public event Action<float> onSecondary;
    public event Action<float> onTertiary;

    private void Start()
    {
        _inputs = new Inputs();

        if (_initialInputType == InputType.Player)
        {
            EnablePlayerInputs(); 
            PlayerInputs();
        }
        else 
        {
            EnableUIInputs();
            UIInputs();
        }
    }

    public void EnablePlayerInputs() => _inputs.Player.Enable();
    public void DisablePlayerInputs() => _inputs.Player.Disable();
    public void EnableUIInputs() => _inputs.UI.Enable();
    public void DisableUIInputs() => _inputs.UI.Disable();

    private void PlayerInputs()
    {
        Inputs.PlayerActions map = _inputs.Player;

        #region --== performed ==--
        map.Movement.performed += ctx => 
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            onMovementUpdate?.Invoke(value);
        };

        map.Look.performed += ctx =>
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            onMousePositionUpdate?.Invoke(value);
        };

        map.Switch.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onMouseScrolled?.Invoke(value);
        };

        map.Throw.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onMouseClicked?.Invoke(value);
        };
        
        map.Dash.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onDash?.Invoke(value);
        };

        map.Primary.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onPrimary?.Invoke(value);
        };

        map.Secondary.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onSecondary?.Invoke(value);
        };

        map.Tertiary.performed += ctx =>
        {
            float value = ctx.ReadValue<float();
            onTertiary?.Invoke(value);
        };
        #endregion
        #region --== canceled ==--
        map.Movement.canceled += ctx => 
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            onMovementUpdate?.Invoke(value);
        };

        map.Look.canceled += ctx =>
        {
            Vector2 value = ctx.ReadValue<Vector2>();
            onMousePositionUpdate?.Invoke(value);
        };

        map.Switch.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onMouseScrolled?.Invoke(value);
        };

        map.Throw.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onMouseClicked?.Invoke(value);
        };
        
        map.Dash.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onDash?.Invoke(value);
        };

        map.Primary.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onPrimary?.Invoke(value);
        };

        map.Secondary.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onSecondary?.Invoke(value);
        };

        map.Tertiary.canceled += ctx =>
        {
            float value = ctx.ReadValue<float();
            onTertiary?.Invoke(value);
        };
        #endregion
    }

    private void UIInputs()
    {

    }
}
