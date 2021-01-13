using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWaitNode : BehaviourNode
{
    private GuardControl _guardControl;

    public GuardWaitNode(GuardControl guardControl)
    {
        _guardControl = guardControl;
    }

    protected override void Run()
    {
        if (_guardControl == null)
            _guardControl = FindObjectOfType<GuardControl>();

        if (_guardControl.WaitAtLocation())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
