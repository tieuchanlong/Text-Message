using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredNode : BehaviourNode
{
    private ScientistControl _scientistControl;

    public TiredNode(ScientistControl scientistControl)
    {
        _scientistControl = scientistControl;
    }

    protected override void Run()
    {
        // Run animation
        if (_scientistControl == null)
            _scientistControl = FindObjectOfType<ScientistControl>();

        if (_scientistControl.IsTired())
            _nodeState = NodeState.RUNNING;
        else
            _nodeState = NodeState.SUCCESS;
    }
}
