using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    InputManager _inputs;

    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

}
