using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistRoom1Node : SequenceNode
{
    private ScientistControl _scientistControl;
    
    public ScientistRoom1Node(ScientistControl scientistControl)
    {
        _scientistControl = scientistControl;
    }
}
