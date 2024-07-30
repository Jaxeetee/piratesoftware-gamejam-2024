using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EntityDamageable
{
    [SerializeField]
    private float _enemyHealth = 50f;
    [SerializeField]
    private float _turnHeadSpeed = .25f;

    private float enemyHealth 
    {
        get => _enemyHealth;
        set => _enemyHealth = Mathf.Clamp(value , 0, 999f);
    }
    private Transform _player;
    private NavMeshAgent _agent;

    private string _poolKey;

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

            Vector3 direction = _agent.velocity.normalized;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(Vector3.forward * angle);  
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _turnHeadSpeed * Time.deltaTime);
            
            _agent.SetDestination(_player.position);
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