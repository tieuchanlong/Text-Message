using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForNextPersonNode : BehaviourNode
{
    private TicketCheckerControl _ticketCheckerControl;

    public WaitForNextPersonNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }

    protected override void Run()
    {
        if (_ticketCheckerControl == null)
            _ticketCheckerControl = FindObjectOfType<TicketCheckerControl>();

        if (_ticketCheckerControl.WaitForNextPerson())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
