using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskForOrderNode : BehaviourNode
{
    private WaiterControl _waiterControl;

    public AskForOrderNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }

    protected override void Run()
    {
        if (_waiterControl == null)
            _waiterControl = FindObjectOfType<WaiterControl>();

        if (_waiterControl.AskOrder())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
