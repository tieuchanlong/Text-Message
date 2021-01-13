using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistWaitNode : BehaviourNode
{
    private ScientistControl _scientistControl;

    public ScientistWaitNode(ScientistControl scientistControl)
    {
        _scientistControl = scientistControl;
    }

    protected override void Run()
    {
        if (_scientistControl == null)
            _scientistControl = FindObjectOfType<ScientistControl>();

        if (_scientistControl.WaitAtLocation())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
