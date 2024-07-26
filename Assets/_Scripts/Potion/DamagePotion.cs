using UnityEngine;
using MyUtils;

public class DamagePotion : Potion
{
    [SerializeField]
    private float _damage;

    private void OnEnable()
    {
        onThrowDelegate = Throw;
    }


    protected override void Throw(Vector3 mousePosition,float maxThrowingDistance)
    {
        var dir = mousePosition - transform.position;
        dir.Normalize();
        GameObject projectile = ObjectPoolManager.Instance.GetObject(_poolKey);
        Projectile thrownPotion = projectile.GetComponent<Projectile>();

        thrownPotion.InitStats(
            _poolKey, 
            _damage, 
            HitType.DAMAGE, 
            maxThrowingDistance,
            transform.position, 
            dir, 
            _affectedLayerMask);
    }


}
