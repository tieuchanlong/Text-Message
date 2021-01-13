using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSeatNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public FindSeatNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.FindSeat())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
