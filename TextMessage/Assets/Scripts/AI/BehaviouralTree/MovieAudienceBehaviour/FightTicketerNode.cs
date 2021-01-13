using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTicketerNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public FightTicketerNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.FightTicketer())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
