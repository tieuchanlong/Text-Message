using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMusicianTurnNode : BehaviourNode
{
    private MusicianControl _musicianControl;

    public CheckMusicianTurnNode(MusicianControl musicianControl)
    {
        _musicianControl = musicianControl;
    }

    protected override void Run()
    {
        if (_musicianControl == null)
            _musicianControl = FindObjectOfType<MusicianControl>();

        if (_musicianControl.CheckTurn())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.FAILURE;
    }
}
