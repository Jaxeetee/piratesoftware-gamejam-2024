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
    private float radius = 2;

    private LayerMask _hitMask;
    private LayerMask _immuneHitMask;

    private float _hitValue; // stores the amount 
    private HitType _hitType;

    private Vector3 _destination;
    private Vector3 _startPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CastExplosion();
    }

    private void CastExplosion()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, _hitMask);

        for (int i = 0; i < hits.Length; i++)
        {
            // float angle = i * (360f / rayCount) * Mathf.Deg2Rad;
            // Vector2 dir = new Vector2(transform.position.x + Mathf.Cos(angle), transform.position.y + Mathf.Sin(angle));
            var hit = Physics2D.RaycastAll(transform.position, hits[i].transform.position, radius, _hitMask);
            
            Debug.DrawLine(transform.position,  hits[i].transform.position, Color.red);
            foreach(var hit2 in hit)
            {
                Debug.DrawLine(transform.position,  hit2.transform.position, Color.green);
            }
            // if (hit.collider != null)
            // {
            //     Debug.DrawLine(transform.position, hit.point , Color.white); // For visualization
            //     // Do something with the hit object
            // }
        }
    }

    private void OnSplash()
    {
        //TODO Commit wizardry
        //* prolly when this projectile hits the location.
        //* this object just travels towards the hitpoint.
        //* Might just use Start Coroutine/async tasks
    }

    private void OnEnd()
    {
        ObjectPoolManager.Instance.ReturnToPool(_poolKey, this.gameObject);
    }

    private IEnumerator GoToDestination()
    {
        var elapsed = 0.0f; 
        while (Vector2.Distance(transform.position, _destination) > .01f)
        {
            elapsed += Time.deltaTime * .5f;
            //Debug.Log(elapsed);
            transform.position = Vector2.Lerp(_startPoint, _destination, elapsed);
            yield return null;
        }
        OnEnd();
    }

    public void InitStats(string poolKey, float hitValue, HitType hitType,Vector3 startPoint ,Vector3 destination, LayerMask hitMask)
    {
        _poolKey = poolKey;

        _hitValue = hitValue;
        _hitType = hitType; 
        _hitMask = hitMask; // Objects that are targetted by the splash damage
        _immuneHitMask = ~ ( 1 << hitMask); // Objects that are immune to the splash damage
        
        _startPoint = startPoint;
        _destination = destination; // final destination of the projectile
        Debug.Log($"start point: {_startPoint} | destination: {_destination}");
        StartCoroutine(GoToDestination());
    }


}
