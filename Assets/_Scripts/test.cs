using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Material mat;    
    public SpriteRenderer sprite;// Start is called before the first frame update
    void Start()
    {
        var testing = sprite.material.GetFloat("_potionLiquidFill");
        Debug.Log(testing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
