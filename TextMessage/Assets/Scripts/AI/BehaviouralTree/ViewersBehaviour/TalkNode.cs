using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkNode : BehaviourNode
{
    private GuestControl _guestControl;

    public TalkNode(GuestControl guestControl)
    {
        _guestControl = guestControl;
    }

    protected override void Run()
    {
        if (_guestControl == null)
            _guestControl = FindObjectOfType<GuestControl>();

        if (_guestControl.Talk())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
