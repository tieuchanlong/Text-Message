using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTableNode : BehaviourNode
{
    private EatingGuestControl _eatingGuestControl;

    public SearchTableNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }

    protected override void Run()
    {
        if (_eatingGuestControl == null)
            _eatingGuestControl = FindObjectOfType<EatingGuestControl>();

        if (_eatingGuestControl.SearchTable())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
