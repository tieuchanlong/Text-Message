using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScientistControl : MonoBehaviour
{
    private ScientistRoom1Node _root;
    private NavMeshAgent _navMesh;
    private bool tired = false;

    [SerializeField]
    private GameObject Table;

    [SerializeField]
    private GameObject Window;

    private float maxWaitTime = 10;
    private float currentWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _root = new ScientistRoom1Node(this);
        PrepareBehaviorTree();
    }

    void PrepareBehaviorTree()
    {
        _root.AddNodes(new WorkAtTableNode(this));
        _root.AddNodes(new ScientistWaitNode(this));
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new LookWindowNode(this));
        _root.AddNodes(new ScientistWaitNode(this));
        (_root.GetNode(2) as SelectorNode).AddNodes(new TiredNode(this));
        (_root.GetNode(2) as SelectorNode).AddNodes(new TakeCoffeeNode());
    }

    // Update is called once per frame
    void Update()
    {
        NodeState state = _root.Evaluate();

        if (state == NodeState.SUCCESS || state == NodeState.FAILURE)
            _root.ResetBehavior();
    }

    public bool MoveToTable()
    {
        _navMesh.SetDestination(Table.transform.position);

        float dist = Vector3.Distance(transform.position, Table.transform.position);

        if (Vector3.Distance(transform.position, Table.transform.position) > 2.0f)
            return false;
        else
            return true;
    }

    public bool IsTired()
    {
        return tired;
    }

    public bool MoveToWindow()
    {
        _navMesh.SetDestination(Window.transform.position);

        if (Vector3.Distance(transform.position, Window.transform.position) > 2.0f)
            return false;
        else
            return true;
    }

    public bool WaitAtLocation()
    {
        currentWaitTime += Time.deltaTime;

        if (currentWaitTime > maxWaitTime)
        {
            currentWaitTime = 0;
            return true;
        }

        return false;
    }
}
