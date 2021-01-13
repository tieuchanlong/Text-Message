using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : BehaviourNode
{
    protected BehaviourNode node;

    public InverterNode(BehaviourNode node)
    {
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }

        return _nodeState;
    }

    protected override void Run()
    {
        // Run animation
    }
}
