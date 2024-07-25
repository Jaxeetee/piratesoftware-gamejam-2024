using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagePotion : Potion
{
    [SerializeField]
    private float _damage;

    protected override void Throw(Vector3 destination)
    {
        // spawn a throwable potion
        Debug.Log($"Potion: {this.name}");
    }


}
