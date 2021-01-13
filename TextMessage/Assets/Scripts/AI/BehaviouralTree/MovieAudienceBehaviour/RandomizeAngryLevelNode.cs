using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAngryLevelNode : BehaviourNode
{
    private MovieAudienceControl _movieAudienceControl;

    public RandomizeAngryLevelNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }

    protected override void Run()
    {
        if (_movieAudienceControl == null)
            _movieAudienceControl = FindObjectOfType<MovieAudienceControl>();

        if (_movieAudienceControl.RandomizeAngryLevel())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
