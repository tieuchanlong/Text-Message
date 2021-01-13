using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : SequenceNode
{
    private GuardControl _guardControl;
    
    public PatrolNode(GuardControl guardControl)
    {
        _guardControl = guardControl;
    }
}
