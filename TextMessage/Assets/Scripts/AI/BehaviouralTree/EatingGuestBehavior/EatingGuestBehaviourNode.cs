using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingGuestBehaviourNode : SequenceNode
{
    private EatingGuestControl _eatingGuestControl;

    public EatingGuestBehaviourNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }
}
