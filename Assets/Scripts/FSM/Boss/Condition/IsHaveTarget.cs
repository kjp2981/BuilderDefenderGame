using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHaveTarget : AICondition
{
    public override bool IfCondition(AIState currentState, AIState nextState)
    {
        return _aiBrain.Target != null;
    }
}
