using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public new static BossEnemy Create(Vector3 position)
    {
        Transform pfEnemy = GameAssets.Instance.pfBossEnemy;
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);
        BossEnemy enemy = enemyTransform.GetComponent<BossEnemy>();
        return enemy;
    }

    private AIBrain aiBrain;

    void Start()
    {
        targetMaxRadius = 60f;
        aiBrain = GetComponentInChildren<AIBrain>();
        aiBrain.SetTarget(TargetTransform.gameObject);
    }
}
