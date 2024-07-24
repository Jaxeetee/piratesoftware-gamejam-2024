using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    WATER,
    EARTH,
    FIRE,
    AIR
}

public class DamagePotion : Potion
{
    [SerializeField]
    private float _damage;

    [SerializeField]
    private DamageType _damageType;

    public DamageType damageType
    {
        get => _damageType;
        set => _damageType = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Throw()
    {
        
    }
}
