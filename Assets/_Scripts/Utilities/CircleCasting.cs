using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleCasting : MonoBehaviour
{

    public float rayCount;
    public float radius = 1f;
    public Collider2D[] collider2Ds;
    public Collider2D[] collider2D1s;
    public LayerMask layerMask;
    public LayerMask testMask;
    public float amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collider2Ds = Physics2D.OverlapCircleAll(transform.position, 1f, layerMask);

        amount = Physics2D.OverlapCircleNonAlloc(transform.position, 1f, collider2Ds, testMask);
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount) * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius, layerMask);

            if (hit.collider != null)   
            {
                Debug.DrawLine(transform.position, hit.point, Color.red); // For visualization
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
