using MyUtils;
using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField]
    private float _healAmount = 5;

    protected override void Throw(Vector3 destination)
    {
        Debug.Log($"Potion: {this.name}");
        //TODO INSTANTIATE PROJECTILE
        GameObject projectile = ObjectPoolManager.GetObject(_poolKey);
        projectile.GetComponent<Projectile>().InitStats(_poolKey, _healAmount, HitType.HEAL, transform.position, destination, _affectedLayerMask);
        
    }



    
}
