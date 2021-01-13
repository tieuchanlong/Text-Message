using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveEatingAreaNode : BehaviourNode
{
    private EatingGuestControl _eatingGuestControl;

    public LeaveEatingAreaNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }

    protected override void Run()
    {
        if (_eatingGuestControl == null)
            _eatingGuestControl = FindObjectOfType<EatingGuestControl>();

        if (_eatingGuestControl.ExitArea())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
