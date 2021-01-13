using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGuardNode : BehaviourNode
{
    private TicketCheckerControl _ticketCheckerControl;

    public CallGuardNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }

    protected override void Run()
    {
        if (_ticketCheckerControl == null)
            _ticketCheckerControl = FindObjectOfType<TicketCheckerControl>();

        if (_ticketCheckerControl.CallGuard())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
