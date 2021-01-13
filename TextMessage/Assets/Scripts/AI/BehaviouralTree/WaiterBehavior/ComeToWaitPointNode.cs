using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeToWaitPointNode : BehaviourNode
{
    private WaiterControl _waiterControl;

    public ComeToWaitPointNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }

    protected override void Run()
    {
        if (_waiterControl == null)
            _waiterControl = FindObjectOfType<WaiterControl>();

        if (_waiterControl.ComeToWaitPoint())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
