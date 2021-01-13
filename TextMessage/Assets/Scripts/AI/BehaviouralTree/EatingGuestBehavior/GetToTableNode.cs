﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetToTableNode : BehaviourNode
{
    private EatingGuestControl _eatingGuestControl;

    public GetToTableNode(EatingGuestControl eatingGuestControl)
    {
        _eatingGuestControl = eatingGuestControl;
    }

    protected override void Run()
    {
        if (_eatingGuestControl == null)
            _eatingGuestControl = FindObjectOfType<EatingGuestControl>();

        if (_eatingGuestControl.GetToTable())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
