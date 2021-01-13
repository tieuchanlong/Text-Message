using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterBehaviorNode : SequenceNode
{
    private WaiterControl _waiterControl;

    public WaiterBehaviorNode(WaiterControl waiterControl)
    {
        _waiterControl = waiterControl;
    }
}
