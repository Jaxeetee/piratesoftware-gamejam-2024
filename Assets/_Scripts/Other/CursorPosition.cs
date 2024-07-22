using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CursorType {
    Gameplay,
    UI
}
public class CursorPosition : MonoBehaviour
{
    [SerializeField]
    private CursorType _cursorType = CursorType.Gameplay;

    [SerializeField]
    private Sprite[] _cursors;


    private SpriteRenderer _spriteRenderer;
    private Camera _mainCam;
    private InputManager _inputs;
    private Vector2 _mouseInputPosition;
    private Vector3 _vector3MousePosition;
    private void Awake()
    {
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputs = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
        if (_cursorType == CursorType.Gameplay)
        {
            //TODO use the cursor for gameplay
            _spriteRenderer.sprite = _cursors[0];
        }
        else 
        {
            _spriteRenderer.sprite = _cursors[1];
        }
    }

    private void OnEnable()
    {
        _inputs.onMousePositionUpdate += OnMousePositionUpdate;
    }

    private void OnDisable()
    {
        _inputs.onMousePositionUpdate -= OnMousePositionUpdate;
    }

    //! CURSOR DELAY
    private void FixedUpdate()
    {
        var mousePos = _mainCam.ScreenToWorldPoint(_mouseInputPosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }


    private bool IsInputNull() => _inputs == null;

    private void OnMousePositionUpdate(Vector2 axis)
    {
        if (IsInputNull()) return;

        _mouseInputPosition = axis;
        _vector3MousePosition = new Vector3(axis.x, axis.y, 0);
    }

}
