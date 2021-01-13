using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFoodNode : BehaviourNode
{
    private EatingGuestControl _eatingGuestControl;

    public OrderFoodNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }

    protected override void Run()
    {
        if (_eatingGuestControl == null)
            _eatingGuestControl = FindObjectOfType<EatingGuestControl>();

        if (_eatingGuestControl.OrderFood())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
