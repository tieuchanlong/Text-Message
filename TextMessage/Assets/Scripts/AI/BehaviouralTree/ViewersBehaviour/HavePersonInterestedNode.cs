using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavePersonInterestedNode : BehaviourNode
{
    private GuestControl _guestControl;

    public HavePersonInterestedNode(GuestControl guestControl)
    {
        _guestControl = guestControl;
    }

    protected override void Run()
    {
        if (_guestControl == null)
            _guestControl = FindObjectOfType<GuestControl>();

        if (_guestControl.IsIterestedPersonClose())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
