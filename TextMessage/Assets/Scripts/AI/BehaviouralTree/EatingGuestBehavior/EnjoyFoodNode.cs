using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnjoyFoodNode : BehaviourNode
{
    private EatingGuestControl _eatingGuestControl;

    public EnjoyFoodNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }

    protected override void Run()
    {
        if (_eatingGuestControl == null)
            _eatingGuestControl = FindObjectOfType<EatingGuestControl>();

        if (_eatingGuestControl.EnjoyFood())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
