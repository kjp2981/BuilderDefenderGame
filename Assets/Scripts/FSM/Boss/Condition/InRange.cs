using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRange : AICondition
{
    [SerializeField]
    private float range;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return Vector2.Distance(transform.position, _aiBrain.Target.transform.position) <= range;
    }
}
