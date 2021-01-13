using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPicNode : BehaviourNode
{
    private GuestControl _guestControl;

    public LookAtPicNode(GuestControl guestControl)
    {
        _guestControl = guestControl;
    }

    protected override void Run()
    {
        if (_guestControl == null)
            _guestControl = FindObjectOfType<GuestControl>();

        if (_guestControl.LookAtPic())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
