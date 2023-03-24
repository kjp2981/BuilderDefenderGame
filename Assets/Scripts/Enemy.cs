using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTramsform = Instantiate(pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTramsform.GetComponent<Enemy>();

        return enemy;
    }

    private Transform targetTransform;
    private Rigidbody2D enemyRigidbody2D;

    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
    }

    private void Update()
    {
        Vector3 moveDir = (targetTransform.position - transform.position).normalized;
        float moveSpeed = 8;
        enemyRigidbody2D.velocity = moveDir * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }
}
