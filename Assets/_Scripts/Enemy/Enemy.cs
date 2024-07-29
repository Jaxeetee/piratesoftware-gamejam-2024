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
            // var direction = _player.position - transform.position;
            // direction.Normalize();
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            _agent.SetDestination(_player.position);
            yield return _waitForSeconds;
        }
    }

    public override void Die()
    {
        Debug.Log($"name: {gameObject.name} died!");
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
        throw new NotImplementedException();
    }

    private void Initialize(string poolKey)
    {

    }
}