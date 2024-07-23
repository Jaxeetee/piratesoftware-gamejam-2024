using UnityEngine;

public class Aim : MonoBehaviour
{
    private InputManager _inputs;
    private Camera _mainCam;

    private Vector3 _mouseInputPos;
    private Vector3 _pos;
    private Vector3 _rot;

    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _inputs.onMousePositionUpdate += OnMousePositionUpdate;
    }

    private void OnDisable()
    {
        _inputs.onMousePositionUpdate -= OnMousePositionUpdate;
    }

    private void Update()
    {
        AimPoint();
    }

    private void AimPoint()
    {
        _pos = _mainCam.ScreenToWorldPoint(_mouseInputPos);
        _rot = _pos - transform.position;
        float rotZ = Mathf.Atan2(_rot.y, _rot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void OnMousePositionUpdate(Vector2 axis)
    {
        if (_inputs == null) return;

        _mouseInputPos = new Vector3(axis.x, axis.y, 0f);
    }
}