using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningNode : BehaviourNode
{
    private TicketCheckerControl _ticketCheckerControl;

    public WarningNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }

    protected override void Run()
    {
        if (_ticketCheckerControl == null)
            _ticketCheckerControl = FindObjectOfType<TicketCheckerControl>();

        if (_ticketCheckerControl.Warning())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
