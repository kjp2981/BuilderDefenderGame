using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowProjectile : MonoBehaviour
{
    //private Enemy targetEnemy;
    private Transform targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 2f;
    public int damageAmount = 10;

    private string tag;

    public static ArrowProjectile Create(Vector3 position, Transform enemy, string tag)
    {
        Transform pfArrowProjectile = GameAssets.Instance.pfArrowProjectile;
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrow = arrowTransform.GetComponent<ArrowProjectile>();
        arrow.SetTarget(enemy);
        arrow.SetTag(tag);

        return arrow;
    }

    private void Start()
    {
        timeToDie = 5f;
    }

    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        transform.eulerAngles = new Vector3(0f, 0f, UtilClass.GetAngleFromVector(moveDir));

        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Transform targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void SetTag(string tag)
    {
        this.tag = tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy enemy = collision.GetComponent<Enemy>();
        HealthSystem hs = collision.GetComponent<HealthSystem>();

        if(hs != null && collision.CompareTag(tag) == true)
        {
            hs.Damage(damageAmount);

            Destroy(gameObject);
        }
    }
}
