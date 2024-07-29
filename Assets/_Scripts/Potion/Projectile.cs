using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;
using UnityEngine.InputSystem;


public enum HitType 
{
    HEAL,
    DAMAGE
}
public class Projectile : MonoBehaviour
{
    private string _poolKey;
    // Start is called before the first frame update

    [SerializeField]
    private int rayCount = 25;

    [SerializeField]
    private float _radius = 1;

    private LayerMask _hitMask;
    private LayerMask _immuneHitMask;

    private float _hitValue; // stores the amount 
    private HitType _hitType;

    private Vector3 _destination;
    private Vector3 _startPoint;
    private void OnSplash()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _hitMask);
        if (_hitType == HitType.HEAL)
        {
            if (hits.Length > 0)
            {
                Debug.Log(hits[0].name);
                hits[0].GetComponent<Player>().Heal(_hitValue);
            }
        }
        

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _radius, _hitMask);

            if (hit.collider != null)   
            {
                if (_hitType == HitType.DAMAGE)
                    hit.collider.GetComponent<EntityDamageable>().ReceiveDamage(_hitValue);
                
            }
        }
        Despawn();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radius);
    }

    private void Despawn()
    {
        ObjectPoolManager.Instance.ReturnToPool(_poolKey, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 0 && other.gameObject.layer != 13)
        {   
            OnSplash();   
        }
    }
    private IEnumerator GoToDestination()
    {
        var elapsed = 0.0f; 

        while (Vector2.Distance(transform.position, _destination) > .01f)
        {
            elapsed += Time.deltaTime * 10f;

            transform.position = Vector2.Lerp(_startPoint, _destination, elapsed);
            yield return null;
        }
        OnSplash();
    }

    public void InitStats(string poolKey, float hitValue, HitType hitType, float maxRange, float radius, Vector3 startPoint ,Vector3 direction, LayerMask hitMask)
    {
        _poolKey = poolKey;

        _hitValue = hitValue;
        _hitType = hitType; 
        _hitMask = hitMask; // Objects that are targetted by the splash damage
        _immuneHitMask = ~ ( 1 << hitMask); // Objects that are immune to the splash damage
        _radius = radius;

        _startPoint = startPoint + (direction / 5);
        _destination = startPoint + direction * maxRange; // final destination of the projectile

        StartCoroutine(GoToDestination());
    }


}
