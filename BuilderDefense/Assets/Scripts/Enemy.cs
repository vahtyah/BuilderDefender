using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        var pdfEnemy = Resources.Load<Transform>("pfEnemy");
        return Instantiate(pdfEnemy, position, Quaternion.identity).GetComponent<Enemy>();
    }


    [SerializeField] private float moveSpeed = 200f;

    private HealthSystem _healthSystem;
    private Transform _targetTransform;
    private Rigidbody2D _rigidbody2D;
    private Transform _hqBuildingTransform;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _hqBuildingTransform = BuildingManager.Instance.HqBuilding.transform;
        _targetTransform = _hqBuildingTransform;
        StartCoroutine(LookForTargets());

        _healthSystem.Died += (sender, args) => { Destroy(gameObject); };
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (_targetTransform == null) return;
        var moveDir = (_targetTransform.position - transform.position).normalized;
        _rigidbody2D.velocity = moveDir * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var building = other.gameObject.GetComponent<Building>();
        if (building != null)
        {
            var healthSystem = building.HealthSystem;
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LookForTargets()
    {
        while (true)
        {
            var targetMaxRadius = 10f;
            var collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
            foreach (var collider2D in collider2Ds)
            {
                var building = collider2D.gameObject.GetComponent<Building>();
                if (building == null) continue;
                if (_targetTransform == null) _targetTransform = building.transform;
                else if (Vector3.Distance(transform.position, building.transform.position) <
                         Vector3.Distance(transform.position, _targetTransform.position))
                    _targetTransform = building.transform;
            }

            if (_targetTransform == null && _hqBuildingTransform != null) _targetTransform = _hqBuildingTransform;

            yield return new WaitForSeconds(.5f);
        }
    }
}