using UnityEngine;

public class Player : EntityDamageable
{
    InputManager _inputs;

    public float actualHealth 
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, 20);
        
    }

    private void Awake()
    {
        _inputs = FindObjectOfType<InputManager>();
    }

    private void OnEnable()
    {
        _inputs.onEscape += PauseInput;
    }

    private void OnDisable()
    {
        _inputs.onEscape -= PauseInput;
    }

    public override void Die()
    {
        //TODO Gameover
    }

    public override void ReceiveDamage(float damage)
    {
        actualHealth -= damage;

        if (actualHealth < 0) Die();
    }

    public override void Heal(float healAmount)
    {
        actualHealth += healAmount;
    }

#region -== INPUTS ==-
    private void PauseInput(float value)
    {
        if (_inputs == null || value == 0) return;

        //didPause = !didPause;

        
    }
#endregion
}
