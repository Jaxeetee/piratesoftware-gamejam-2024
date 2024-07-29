using UnityEngine;

public abstract class EntityDamageable : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _startHealth = 20;
    
    protected float _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }
    public abstract void Die();
    public abstract void ReceiveDamage(float damage);
    public abstract void Heal(float healAmount);
}