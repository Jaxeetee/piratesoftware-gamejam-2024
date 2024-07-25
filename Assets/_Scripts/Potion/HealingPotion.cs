using MyUtils;
using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField]
    private float _healAmount = 5;


    private void OnEnable()
    {
        onThrowDelegate = Throw;
    }

    public void Init()
    {
        ObjectPoolManager.Instance.CreatePool(_poolKey, _throwablePotion.gameObject, this.gameObject);
    }
    protected override void Throw(Vector3 destination)
    {
        Debug.Log($"Potion: {this.name}");
        //TODO INSTANTIATE PROJECTILE
        GameObject projectile = ObjectPoolManager.Instance.GetObject(_poolKey);
        projectile.GetComponent<Projectile>().InitStats(_poolKey, _healAmount, HitType.HEAL, transform.position, destination, _affectedLayerMask);
        
    }



    
}
