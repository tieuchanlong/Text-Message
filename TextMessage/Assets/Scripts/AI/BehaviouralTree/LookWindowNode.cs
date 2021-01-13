using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWindowNode : BehaviourNode
{
    private ScientistControl _scientistControl;

    public LookWindowNode(ScientistControl scientistControl)
    {
        _scientistControl = scientistControl;
    }

    protected override void Run()
    {
        // Run animation
        if (_scientistControl == null)
            _scientistControl = FindObjectOfType<ScientistControl>();

        if (_scientistControl.MoveToWindow())
            _nodeState = NodeState.SUCCESS;
    }
}
