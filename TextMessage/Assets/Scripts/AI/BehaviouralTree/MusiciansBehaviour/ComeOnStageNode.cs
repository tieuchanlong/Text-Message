using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOnStageNode : BehaviourNode
{
    private MusicianControl _musicianControl;

    public ComeOnStageNode(MusicianControl musicianControl)
    {
        _musicianControl = musicianControl;
    }

    protected override void Run()
    {
        if (_musicianControl == null)
            _musicianControl = FindObjectOfType<MusicianControl>();

        if (_musicianControl.ComeOnStage())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
