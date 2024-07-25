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

    protected override void Throw(Vector3 destination)
    {
        // spawn a throwable potion
        Debug.Log($"Potion: {this.name}");
        GameObject projectile = ObjectPoolManager.Instance.GetObject(_poolKey);
        projectile.GetComponent<Projectile>().InitStats(_poolKey, _damage, HitType.DAMAGE, transform.position, destination, _affectedLayerMask);
    }


}
