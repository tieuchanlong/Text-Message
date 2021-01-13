using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLineNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public GoToLineNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.GoToLine())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
