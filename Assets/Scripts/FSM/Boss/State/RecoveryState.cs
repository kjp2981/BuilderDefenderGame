using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : AIState
{
    private HealthSystem hs;
    private BossEnemy _enemy;

    private float timer = 0f;

    protected override void Awake()
    {
        base.Awake();
        hs = GetComponentInParent<HealthSystem>();
        _enemy = GetComponentInParent<BossEnemy>();
    }

    public override void OnStateEnter()
    {
        if(hs == null)
        {
            hs = GetComponentInParent<HealthSystem>();
        }
        if (_enemy == null)
        {
            _enemy = GetComponentInParent<BossEnemy>();
        }
        _enemy.moveSpeed = 0f;
        timer = 0f;
    }

    public override void TakeAAction()
    {
        timer += Time.deltaTime;

        if(timer >= 1f)
        {
            hs.Heal(10);
            timer = 0f;
        }
    }
}
