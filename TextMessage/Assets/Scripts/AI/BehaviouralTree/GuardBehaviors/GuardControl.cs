using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardControl : MonoBehaviour
{
    private PatrolNode _root;
    private NavMeshAgent _agent;

    private float maxWaitTime = 10;
    private float currentWaitTime;

    [SerializeField]
    private List<GameObject> PatrolPoints;
    private int currentPatrolPoint = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        gameObject.AddComponent(typeof(EquipmentControl));
        gameObject.AddComponent(typeof(NPCStateControl));
        PrepareBehaviorTree();
    }

    private void PrepareBehaviorTree()
    {
        _root = new PatrolNode(this);
        _root.AddNodes(new GoToPatrolPointNode(this));
        _root.AddNodes(new GuardWaitNode(this));
    }

    // Update is called once per frame
    void Update()
    {
        NodeState state = _root.Evaluate();

        if (state == NodeState.SUCCESS || state == NodeState.FAILURE)
            _root.ResetBehavior();
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

    public bool GoToPatrolPoint()
    {
        if (currentPatrolPoint == -1)
        {
            currentPatrolPoint = Random.Range(0, PatrolPoints.Count);
            _agent.SetDestination(PatrolPoints[currentPatrolPoint].transform.position);
        }

        if (Vector3.Distance(transform.position, PatrolPoints[currentPatrolPoint].transform.position) > 2.0f)
            return true;
        else
            currentPatrolPoint = -1;

        return false;
    }
}
