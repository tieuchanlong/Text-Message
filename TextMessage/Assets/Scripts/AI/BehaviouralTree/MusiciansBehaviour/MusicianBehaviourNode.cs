using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianBehaviourNode : SequenceNode
{
    private MusicianControl _musicianControl;

    public MusicianBehaviourNode(MusicianControl musicianControl)
    {
        _musicianControl = musicianControl;
    }
}
