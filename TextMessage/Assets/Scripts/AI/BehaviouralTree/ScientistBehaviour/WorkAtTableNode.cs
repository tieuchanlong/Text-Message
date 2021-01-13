using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkAtTableNode : BehaviourNode
{
    private ScientistControl _scientistControl;

    public WorkAtTableNode(ScientistControl scientistControl)
    {
        _scientistControl = scientistControl;
    }

    protected override void Run()
    {
        // Run animation
        if (_scientistControl == null)
            _scientistControl = FindObjectOfType<ScientistControl>();

        if (_scientistControl.MoveToTable())
            _nodeState = NodeState.SUCCESS;
    }
}
