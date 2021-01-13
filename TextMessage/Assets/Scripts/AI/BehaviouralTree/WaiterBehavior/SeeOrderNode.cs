using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeOrderNode : BehaviourNode
{
    private WaiterControl _waiterControl;

    public SeeOrderNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }

    protected override void Run()
    {
        if (_waiterControl == null)
            _waiterControl = FindObjectOfType<WaiterControl>();

        if (_waiterControl.SeeOrder())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
