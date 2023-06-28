using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;
    
    private float _shootTimer;
    private Enemy _targetEnemy;
    private Vector3 _projectileSpawnPosition;

    private void Awake()
    {
        _projectileSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
        StartCoroutine(LookForTargets());
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if (_targetEnemy != null && _shootTimer <= 0)
        {
            ArrowProjectile.Create(_targetEnemy, _projectileSpawnPosition);
            _shootTimer = shootTimerMax;
        }
    }

    private IEnumerator LookForTargets()
    {
        while (true)
        {
            var targetMaxRadius = 20f;
            var collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
            foreach (var collider2D in collider2Ds)
            {
                var enemy = collider2D.gameObject.GetComponent<Enemy>();
                if (enemy == null) continue;
                if (_targetEnemy == null) _targetEnemy = enemy;
                else if (Vector3.Distance(transform.position, enemy.transform.position) <
                         Vector3.Distance(transform.position, _targetEnemy.transform.position))
                    _targetEnemy = enemy;
            }

            yield return new WaitForSeconds(.5f);
        }
    }
}