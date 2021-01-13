using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility : MonoBehaviour
{
    private DialogueGraphview _targetGraphView;
    private DialogueCotainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphview targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };


    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any())
            return;

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueCotainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        // Save connection between nodes
        for (int i = 0;i < connectedPorts.Length;i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGUID = inputNode.GUID
            });
        }

        // Save nodes
        foreach(var dialogueNode in Nodes.Where(node=>!node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                Guid = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                Position = dialogueNode.GetPosition().position
            });
        }

        // If Resource folder doesn't exist, create one
        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueCotainer>(fileName);

        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graoh file does not exist!", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        for (var i = 0;i < Nodes.Count;i++)
        {
            var connections = _containerCache.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[i].GUID).ToList();
            for (var j = 0;j < connections.Count;j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGUID;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(
                    _containerCache.DialogueNodeData.First(x => x.Guid == targetNodeGuid).Position,
                    _targetGraphView.defaultNodeSize
                ));
            }
        }
    }

    private void LinkNodes(Port port1, Port port2)
    {
        var _tempEdge = new Edge
        {
            output = port1,
            input = port2
        };

        _tempEdge.input.Connect(_tempEdge);
        _tempEdge.output.Connect(_tempEdge);
        _targetGraphView.Add(_tempEdge);
    }

    private void CreateNodes()
    {
        foreach(var nodeData in _containerCache.DialogueNodeData)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(x => x.BaseNodeGUID == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ClearGraph()
    {
        // Set entry points guid back from the save. Discard existing guid
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGUID;

        // Remove edges connected to this node
        foreach(var node in Nodes)
        {
            if (node.EntryPoint) continue;

            Edges.Where(x => x.input.node == node).ToList().
                ForEach(edge => _targetGraphView.RemoveElement(edge));

            // Then remove node from the graph
            _targetGraphView.RemoveElement(node);
        }
    }
}
