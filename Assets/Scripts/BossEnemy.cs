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

    protected override void Awake()
    {
        base.Awake();
        aiBrain = GetComponentInChildren<AIBrain>();
        if(TargetTransform == null)
        {
            aiBrain.SetTarget(null);
        }
        else
        {
            aiBrain.SetTarget(TargetTransform.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (TargetTransform == null)
        {
            if (BuildingManager.Instance.GetHQBuilding() != null)
                aiBrain.SetTarget(BuildingManager.Instance.GetHQBuilding().gameObject);
            else
                aiBrain.SetTarget(null);
        }
        else
        {
            aiBrain.SetTarget(TargetTransform.gameObject);
        }
    }
}
