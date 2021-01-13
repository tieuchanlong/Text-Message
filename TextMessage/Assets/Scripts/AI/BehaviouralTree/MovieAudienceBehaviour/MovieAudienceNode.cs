using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieAudienceNode : SequenceNode
{
    private MovieAudienceControl _movieAudienceControl;

    public MovieAudienceNode(MovieAudienceControl movieAudienceControl)
    {
        _movieAudienceControl = movieAudienceControl;
    }
}
