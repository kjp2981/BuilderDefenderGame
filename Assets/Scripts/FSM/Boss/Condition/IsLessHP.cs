using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLessHP : AICondition
{
    private HealthSystem hs;

    [SerializeField]
    private int lessValue;

    protected override void Awake()
    {
        base.Awake();
        this.hs = GetComponentInParent<HealthSystem>();
    }

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return hs.Health <= 30;
    }
}
