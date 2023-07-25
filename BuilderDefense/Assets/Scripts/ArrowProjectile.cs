using System;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile Create(Enemy targetEnemy, Vector3 position)
    {
        var pdfArrowProjectile = GameAssets.Instance.pfArrowProjectile;
        var arrowProjectile = Instantiate(pdfArrowProjectile, position, Quaternion.identity)
            .GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(targetEnemy);
        return arrowProjectile;
    }

    [SerializeField] private float moveSpeed = 20f;
    
    private Enemy _targetEnemy;
    private Vector3 _lastMoveDir;
    private float _timeToDie = 2f;


    private void Update()
    {
        Vector3 moveDir;
        if (_targetEnemy != null)
        {
            moveDir = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDir = moveDir;
        }

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(_lastMoveDir));
        transform.position += _lastMoveDir * (moveSpeed * Time.deltaTime);

        _timeToDie -= Time.deltaTime;
        if (_timeToDie <= 0) Destroy(gameObject);
    }

    private void SetTarget(Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            var healthSystem = enemy.gameObject.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }
}