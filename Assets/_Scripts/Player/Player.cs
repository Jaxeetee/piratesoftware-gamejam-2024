public class Player : EntityDamageable
{
    InputManager _inputs;

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
        _currentHealth -= damage;

        if (_currentHealth < 0) Die();
    }

    public override void Heal(float healAmount)
    {
        _currentHealth += healAmount;
    }

    #region -== INPUTS ==-
    private void PauseInput(float value)
    {
        if (_inputs == null || value == 0) return;

        //didPause = !didPause;

        
    }
#endregion
}
