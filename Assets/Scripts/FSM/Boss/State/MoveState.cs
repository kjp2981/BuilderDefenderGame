using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AIState
{
    private BossEnemy _enemy;

    protected override void Awake()
    {
        base.Awake();
        _enemy = GetComponentInParent<BossEnemy>();
    }

    public override void OnStateEnter()
    {
        if (_enemy == null)
        {
            _enemy = GetComponentInParent<BossEnemy>();
        }
        _enemy.moveSpeed = 15f;
    }

    public override void TakeAAction()
    {

    }
}
