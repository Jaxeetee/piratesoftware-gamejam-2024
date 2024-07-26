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
    public delegate void OnThrowDelegate(Vector3 mousePosition, float maxThrowingDistance);
    public OnThrowDelegate onThrowDelegate;


    private void Start()
    {
        ObjectPoolManager.Instance.CreatePool(_poolKey, _throwablePotion.gameObject, this.gameObject);
    }

    private void OnEnable()
    {
        onThrowDelegate = Throw;
    }
    protected abstract void Throw(Vector3 mousePosition, float maxThrowingDistance);

}
