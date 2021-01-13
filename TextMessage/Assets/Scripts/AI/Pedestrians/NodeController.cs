using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public List<NodeController> ConnectedNodes;
    [HideInInspector]
    public int fScore = 0;

    [SerializeField]
    private bool showNode = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = showNode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        foreach(NodeController node in ConnectedNodes)
        {
            if (node == null)
                continue;

            Gizmos.DrawLine(transform.position, node.transform.position);
            Gizmos.color = new Color(0, 1, 0.8f);
        }
    }
}
