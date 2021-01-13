using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPictureNode : BehaviourNode
{
    private GuestControl _guestControl;

    public MoveToPictureNode(GuestControl guestControl)
    {
        _guestControl = guestControl;
    }

    protected override void Run()
    {
        if (_guestControl == null)
            _guestControl = FindObjectOfType<GuestControl>();

        if (_guestControl.GoToPicPoint())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
