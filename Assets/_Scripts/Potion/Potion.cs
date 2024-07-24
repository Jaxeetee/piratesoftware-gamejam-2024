using UnityEngine;

public abstract class Potion : MonoBehaviour
{

    [SerializeField]
    protected float _maxRange;
    [SerializeField]
    protected float _radiusEffect;

    public delegate void OnThrowDelegate();
    public OnThrowDelegate onThrowDelegate;

    private void OnEnable()
    {
        onThrowDelegate = Throw;
    }
    protected abstract void Throw();
    
}
