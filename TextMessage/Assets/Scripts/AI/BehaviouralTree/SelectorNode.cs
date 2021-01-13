using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviourNode
{
    protected List<BehaviourNode> nodes = new List<BehaviourNode>();

    public SelectorNode(List<BehaviourNode> nodes)
    {
        this.nodes = nodes;
    }

    public SelectorNode()
    {

    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }
        _nodeState = NodeState.FAILURE;
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
