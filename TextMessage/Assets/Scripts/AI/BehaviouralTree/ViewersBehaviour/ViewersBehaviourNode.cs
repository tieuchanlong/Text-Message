using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewersBehaviourNode : SequenceNode
{
    private GuestControl _guestControl;

    public ViewersBehaviourNode(GuestControl guestControl)
    {
        _guestControl = guestControl;
    }
}
