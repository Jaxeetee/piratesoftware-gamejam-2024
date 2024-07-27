using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EntityDamageable
{
    [SerializeField]
    private float _enemyHealth = 50f;

    private float enemyHealth 
    {
        get => _enemyHealth;
        set => _enemyHealth = Mathf.Clamp(value , 0, 999f);
    }
    private Transform _player;
    private NavMeshAgent _agent;

    private float _refreshRate = .25f;
    private WaitForSeconds _waitForSeconds;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
		_agent.updateUpAxis = false;
    }

    private void OnEnable()
    {
        _waitForSeconds = new WaitForSeconds(_refreshRate);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while(_player != null)
        {
            _agent.SetDestination(_player.position);
            yield return _waitForSeconds;
        }
    }

    public override void Die()
    {
        
    }

    public override void ReceiveDamage(float damage)
    {
        enemyHealth -= damage;
    }

    public override void Heal(float healAmount)
    {
        throw new NotImplementedException();
    }

    private void Initialize()
    {

    }
}


    // [SerializeField]
    // private float _movementSpeed;

    
    // [SerializeField, Range(0.01f, 1.00f)]
    // private float _refreshRate = 0.25f;

    // [SerializeField]
    // private LayerMask _obstacleMask;
    // private Transform _target;
    // private WaitForSeconds _waitForSeconds;
    // private Vector2 _direction;
    // private Rigidbody2D _rb2d;

    // private void Awake()
    // {
    //     _rb2d = GetComponent<Rigidbody2D>();
    // }
    // // Start is called before the first frame update
    // void Start()
    // {
    //     _waitForSeconds = new WaitForSeconds(_refreshRate);
    //     _target = GameObject.FindGameObjectWithTag("Player").transform;
    // }

    // private void OnEnable()
    // {
    //     //StartCoroutine(FollowPlayer());
    // }

    // private void FixedUpdate()
    // {
    //     Vector2 desiredDirection = (_target.position - transform.position).normalized;
    //     Vector2 avoidDirection = Vector2.zero;
    //     _direction = _target.position - transform.position;
    //     _direction.Normalize();
    //     float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
    //     // Cast multiple rays
    //     for (int i = 0; i < 3; i++)
    //     {
    //         float rayAngle =  UnityEngine.Random.Range(-30, 30) * Mathf.Deg2Rad;
    //         Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //         RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, _obstacleMask);
    //         if (hit.collider != null)
    //         {
    //             avoidDirection += (Vector2)hit.normal;
    //         }
    //     }

    // // Combine desired and avoid directions
    //     _rb2d.velocity = (desiredDirection + avoidDirection).normalized * _movementSpeed;
    // }
    // private IEnumerator FollowPlayer()
    // {
    //     while(_target != null)
    //     {

    //         _direction = _target.position - transform.position;
    //         _direction.Normalize();
    //         float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

    //         transform.position = Vector2.MoveTowards(transform.position, _target.position, _movementSpeed * Time.deltaTime);
    //         transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    //         yield return null;
    //     }
    // }