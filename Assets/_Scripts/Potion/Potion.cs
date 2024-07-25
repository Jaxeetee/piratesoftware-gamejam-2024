using System.Runtime.InteropServices.WindowsRuntime;
using MyUtils;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{

    [SerializeField]
    protected float _maxRange;
    [SerializeField]
    protected float _radiusEffect;

    [SerializeField]
    protected Projectile _throwablePotion;

    [SerializeField]
    protected LayerMask _affectedLayerMask;

    [SerializeField]
    protected string _poolKey;
    public delegate void OnThrowDelegate(Vector3 destination);
    public OnThrowDelegate onThrowDelegate;

    private void OnEnable()
    {
        ObjectPoolManager.CreatePool(_poolKey, _throwablePotion.gameObject, this.gameObject);
        onThrowDelegate = Throw;
    }
    protected abstract void Throw(Vector3 destination);

}
