using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGoInCinemaNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public CanGoInCinemaNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.CanGoInCinema() == 1)
            _nodeState = NodeState.SUCCESS;
        else if (_movieAudienceControl.CanGoInCinema() == 0)
            _nodeState = NodeState.RUNNING;
        else
            _nodeState = NodeState.FAILURE;
    }
}
