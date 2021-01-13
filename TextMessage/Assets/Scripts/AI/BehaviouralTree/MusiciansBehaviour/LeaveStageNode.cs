using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveStageNode : BehaviourNode
{
    private MusicianControl _musicianControl;

    public LeaveStageNode(MusicianControl musicianControl)
    {
        _musicianControl = musicianControl;
    }

    protected override void Run()
    {
        if (_musicianControl == null)
            _musicianControl = FindObjectOfType<MusicianControl>();

        if (_musicianControl.LeaveStage())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
