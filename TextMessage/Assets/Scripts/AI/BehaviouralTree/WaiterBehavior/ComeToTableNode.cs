using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeToTableNode : BehaviourNode
{
    private WaiterControl _waiterControl;

    public ComeToTableNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }

    protected override void Run()
    {
        if (_waiterControl == null)
            _waiterControl = FindObjectOfType<WaiterControl>();

        if (_waiterControl.ComeToTable())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
