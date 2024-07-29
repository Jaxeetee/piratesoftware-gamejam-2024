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
    protected override void Throw(Vector3 mousePosition, float maxThrowingDistance)
    {
        Vector3 dir = mousePosition - transform.position;
        dir.Normalize();
        
        GameObject projectile = ObjectPoolManager.Instance.GetObject(_poolKey);
        Projectile thrownPotion = projectile.GetComponent<Projectile>();
        thrownPotion.InitStats(
            _poolKey, 
            _healAmount, 
            HitType.HEAL, 
            maxThrowingDistance, 
            _radiusEffect,
            transform.position, 
            dir,
            _affectedLayerMask);
        
    }



    
}
