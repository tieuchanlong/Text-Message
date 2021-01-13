using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeTicketNode : BehaviourNode
{
    private TicketCheckerControl _ticketCheckerControl;

    public SeeTicketNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }

    protected override void Run()
    {
        if (_ticketCheckerControl == null)
            _ticketCheckerControl = FindObjectOfType<TicketCheckerControl>();

        if (_ticketCheckerControl.SeeTicket() == 1)
            _nodeState = NodeState.SUCCESS;
        else if (_ticketCheckerControl.SeeTicket() == 0)
            _nodeState = NodeState.RUNNING;
        else
            _nodeState = NodeState.FAILURE;
    }
}
