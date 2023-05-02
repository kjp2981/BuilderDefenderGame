using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTimer : AICondition
{
    [SerializeField]
    private float timer = 3f;

    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return _aiBrain.StateDuractionTime >= timer;
    }
}
