using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Chase,
    Attack
}
public class Enemy : EntityDamageable
{
    [SerializeField]
    private float _enemyHealth = 50f;
    [SerializeField]
    private float _turnHeadSpeed = .25f;
    [SerializeField]
    private float _attackCooldown = 1f;
    [SerializeField]
    private float _distanceToAttack = 1.0f;
    private float _nextTimeToAttack = 0.0f;

    private float enemyHealth 
    {
        get => _enemyHealth;
        set => _enemyHealth = Mathf.Clamp(value , 0, 999f);
    }
    private Transform _player;
    private NavMeshAgent _agent;
    private string _poolKey;
    private float _refreshRate = .25f; //* updates the target position at a rate as SetDestination can be expensive
    private WaitForSeconds _waitForSeconds;
    private EnemyState _currentState;

    private float _targetRadius;
    private float _halfSize;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
		_agent.updateUpAxis = false;
        var size = GetComponent<BoxCollider2D>().size;
        _halfSize = size.x / 2 + size.y / 2;
    }

    private void OnEnable()
    {
        _waitForSeconds = new WaitForSeconds(_refreshRate);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _targetRadius = _player.GetComponent<CapsuleCollider2D>().size.x / 2;
        _currentState = EnemyState.Chase;
        StartCoroutine(FollowPlayer());
    }

    private void Update()
    {
        if (Time.time > _nextTimeToAttack)
        {
            
            float distanceToTarget = (_player.position - transform.position).sqrMagnitude;
            if (distanceToTarget < _distanceToAttack * _distanceToAttack)
            {
                _nextTimeToAttack = Time.time + _attackCooldown;
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        Debug.Log("Is attacking");
        _currentState = EnemyState.Attack;
        _agent.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = _player.position;

        float attackSpeed = 3f;
        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation =  (-percent * percent + percent) * 3; //* this handles the moving towards the player attacking
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);  
            yield return null;
        }
        _agent.enabled = true;
        _currentState = EnemyState.Chase;
    }
    private IEnumerator FollowPlayer()
    {
        while(_player != null)
        {
            Debug.Log("Is Chasing");
            if (_currentState == EnemyState.Chase)
            {
                Vector3 direction = _agent.velocity.normalized;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.Euler(Vector3.forward * angle);  
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _turnHeadSpeed * Time.deltaTime);
                
                Vector3 dirToTarget = (_player.position - transform.position).normalized;
                Vector3 targetPosition = _player.position - dirToTarget * (_halfSize + _targetRadius);
                _agent.SetDestination(targetPosition);

            }
            yield return _waitForSeconds;
        }
    }

    public override void Die()
    {
        ObjectPoolManager.Instance.ReturnToPool(_poolKey, this.gameObject);
    }

    public override void ReceiveDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    public override void Heal(float healAmount)
    {
       
    }

    public void Initialize(string poolKey)
    {
        _poolKey = poolKey;
    }
}