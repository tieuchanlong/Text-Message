using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForOrderNode : BehaviourNode
{
    private WaiterControl _waiterControl;

    public WaitForOrderNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }

    protected override void Run()
    {
        if (_waiterControl == null)
            _waiterControl = FindObjectOfType<WaiterControl>();

        if (_waiterControl.WaitForOrder())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
