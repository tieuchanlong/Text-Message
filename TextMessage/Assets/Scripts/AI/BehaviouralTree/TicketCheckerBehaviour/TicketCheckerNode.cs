using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCheckerNode : SelectorNode
{
    private TicketCheckerControl _ticketCheckerControl;
    
    public TicketCheckerNode(TicketCheckerControl ticketCheckerControl)
    {
        _ticketCheckerControl = ticketCheckerControl;
    }
}
