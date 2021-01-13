﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAreaNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public ExitAreaNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.ExitArea())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
