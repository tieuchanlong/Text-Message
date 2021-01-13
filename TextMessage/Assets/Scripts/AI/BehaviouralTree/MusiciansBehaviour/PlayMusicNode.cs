using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicNode : BehaviourNode
{
    private MusicianControl _musicianControl;

    public PlayMusicNode(MusicianControl musicianControl)
    {
        _musicianControl = musicianControl;
    }

    protected override void Run()
    {
        if (_musicianControl == null)
            _musicianControl = FindObjectOfType<MusicianControl>();

        if (_musicianControl.PlayMusic())
            _nodeState = NodeState.SUCCESS;
        else
            _nodeState = NodeState.RUNNING;
    }
}
