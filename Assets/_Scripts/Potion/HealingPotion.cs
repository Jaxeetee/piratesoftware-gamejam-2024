using MyUtils;
using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField]
    private float _healAmount = 5;

    Vector3 _dir;
    Vector3 _mousePos;

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
        Debug.Log($"Potion: {dir} | mousePosition: {mousePosition}");
        _mousePos = mousePosition;
        //TODO INSTANTIATE PROJECTILE
        GameObject projectile = ObjectPoolManager.Instance.GetObject(_poolKey);
        Projectile thrownPotion = projectile.GetComponent<Projectile>();
        thrownPotion.InitStats(
            _poolKey, 
            _healAmount, 
            HitType.HEAL, 
            maxThrowingDistance, 
            transform.position, 
            dir,
            _affectedLayerMask);
        
    }



    
}
