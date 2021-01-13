using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode: BehaviourNode
{
    protected List<BehaviourNode> nodes = new List<BehaviourNode>();
    
    public SequenceNode(List<BehaviourNode> nodes)
    {
        this.nodes = nodes;
    }

    public SequenceNode()
    {

    }

    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;

        foreach(var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                    break;
                default:
                    break;
            }

            if (isAnyNodeRunning)
                break;
        }
        _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }

    protected override void Run()
    {
        // Run animation
    }

    public override void ResetBehavior()
    {
        base.ResetBehavior();
        _nodeState = NodeState.RUNNING;

        foreach (var node in nodes)
            node.ResetBehavior();
    }

    public void AddNodes(BehaviourNode new_node)
    {
        nodes.Add(new_node);
    }

    public BehaviourNode GetNode(int ind)
    {
        return nodes[ind];
    }

}
