using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    private BossEnemy _enemy;

    private float timer = 0f;

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
        _enemy.moveSpeed = 0f;
        timer = 0;
    }

    public override void TakeAAction()
    {
        timer += Time.deltaTime;

        if(timer >= .2f)
        {
            //Vector3 pos = _aiBrain.Target.transform.position - this.transform.position;
            //pos.Normalize();
            //pos *= 4f;
            //pos.z = 0;
            ArrowProjectile.Create(this.transform.position, _enemy.TargetTransform, "Player");
            timer = 0f;
        }
    }
}
