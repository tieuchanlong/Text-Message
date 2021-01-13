using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowInCinemaNode : BehaviourNode
{
    private TicketCheckerControl _ticketCheckerControl;

    public AllowInCinemaNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }

    protected override void Run()
    {
        if (_ticketCheckerControl == null)
            _ticketCheckerControl = FindObjectOfType<TicketCheckerControl>();

        if (_ticketCheckerControl.AllowIn())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
