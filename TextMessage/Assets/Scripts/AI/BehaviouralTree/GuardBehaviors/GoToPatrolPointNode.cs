using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPatrolPointNode : BehaviourNode
{
    private GuardControl _guardControl;

    public GoToPatrolPointNode(GuardControl guardControl)
    {
        _guardControl = guardControl;
    }

    protected override void Run()
    {
        if (_guardControl == null)
            _guardControl = FindObjectOfType<GuardControl>();

        if (_guardControl.GoToPatrolPoint())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
