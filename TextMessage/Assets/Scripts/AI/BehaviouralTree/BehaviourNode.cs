using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourNode: MonoBehaviour 
{
    protected NodeState _nodeState;

    public BehaviourNode()
    {
        _nodeState = NodeState.RUNNING;
    }

    public NodeState nodeState
    {
        get
        {
            return _nodeState;
        }
    }

    public virtual NodeState Evaluate()
    {
        if (_nodeState == NodeState.RUNNING)
        {
            Run();
        }

        return _nodeState;
    }

    protected abstract void Run();

    public virtual void ResetBehavior()
    {
        _nodeState = NodeState.RUNNING;
    }

}

public enum NodeState
{
    RUNNING, SUCCESS, FAILURE
}
