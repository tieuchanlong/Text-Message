using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    private NodeController startPoint;

    [SerializeField]
    private NodeController goalPoint;
    [HideInInspector]
    public NodeController target;

    private NodeController currentNode;
    private NodeController prevNode;
    private NodeController nextNode;
    [HideInInspector]
    public Dictionary<NodeController, NodeController> cameFrom = new Dictionary<NodeController, NodeController>();

    private ThirdPersonCharacter character;

    private List<NodeController> portionPath = new List<NodeController>();

    private static int MAXMOVE = 8;

    private bool reachedDest = false;

    public bool reachedGoal = false;

    private float speed = 0f;

    private Animator anim;

    private SubwayControl _subwayControl;

    [HideInInspector]
    public Dictionary<NodeController, int> gScore = new Dictionary<NodeController, int>();
    [HideInInspector]
    public Dictionary<NodeController, int> fScore = new Dictionary<NodeController, int>();
    [HideInInspector]
    public PriorityQueue openSet = new PriorityQueue();
    [HideInInspector]
    public Dictionary<NodeController, int> closedSet = new Dictionary<NodeController, int>();

    private void Awake()
    {
        currentNode = startPoint;
        target = startPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        anim = GetComponent<Animator>();
        transform.position = startPoint.transform.position;
        _subwayControl = FindObjectOfType<SubwayControl>();

        if (startPoint != null)
            agent.SetDestination(startPoint.transform.position);
        currentNode = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(new Vector3(UnityEngine.Random.Range(agent.desiredVelocity.x, 2 * agent.desiredVelocity.x),
                        UnityEngine.Random.Range(agent.desiredVelocity.y, 2 * agent.desiredVelocity.y),
                        UnityEngine.Random.Range(agent.desiredVelocity.z, 2 * agent.desiredVelocity.z))
                        , false, false);
            speed = 10;
        }
        else
        { 
            character.Move(Vector3.zero, false, false);
            speed = -1;
        }

        if (ReachedGoal())
            transform.LookAt(_subwayControl.transform);

        SetAnimation();
    }

    private void SetAnimation()
    {
        anim.SetFloat("Speed", speed);
    }

    public NodeController GetCurrentNode()
    {
        return currentNode;
    }

    public void SetCurrentNode(NodeController new_current)
    {
        currentNode = new_current;
    }

    public NodeController GetStart()
    {
        return startPoint;
    }

    public NodeController GetTarget()
    {
        return target;
    }

    public NodeController GetGoal()
    {
        return goalPoint;
    }

    public void SetStartAndGoalPoint()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        currentNode = startPoint;

        StartNodeControl[] startPoints = FindObjectsOfType<StartNodeControl>();
        NodeController[] endPoints = FindObjectsOfType<NodeController>();

        if (startPoint == null)
        {
            startPoint = startPoints[Random.Range(0, startPoints.Length)];
            agent.SetDestination(startPoint.transform.position);
        }

        if (goalPoint == null)
        {
            goalPoint = endPoints[Random.Range(0, endPoints.Length)];

            while (goalPoint.gameObject == startPoint.gameObject)
            {
                goalPoint = endPoints[Random.Range(0, endPoints.Length)];
            }
        }

        transform.position = startPoint.transform.position;
    }

    public void SetPortionPath(ref List<Dictionary<NodeController, PlayerController>> spaceMap)
    {
        int steps_left = MAXMOVE - 1;

        // In case of current null
        if (currentNode == null)
            currentNode = startPoint;

        portionPath.Clear();

        NodeController current = currentNode;
        NodeController prev = prevNode;
        NodeController next_best = current;

        if (current == goalPoint)
        {
            for (int i = 0;i < MAXMOVE;i++)
                portionPath.Add(goalPoint);
            return;
        }

        portionPath.Add(currentNode);

        for (int i = 0;i < MAXMOVE; i++)
        {
            if (current == goalPoint)
                break;

            if (!cameFrom.ContainsKey(current))
                break;

            next_best = cameFrom[current];

            // next best node is not occupied
            if (!spaceMap[i].ContainsKey(next_best))
            {
                PlayerController search1 = (spaceMap[i].ContainsKey(current)) ? spaceMap[i][current] : null;
                    
            // If there is an agent walking towards me
                if (search1 != null)
                {
                    PlayerController another_agent = search1;

                    PlayerController search2 = (i - 1 >= 0 && spaceMap[i - 1].ContainsKey(next_best)) ? spaceMap[i - 1][next_best] : null;

                    // If there is an agent walking towards this next_best node
                    if (search1 == search2)
                    {
                        // Find another best node
                        int current_best_gScore = gScore[current];
                        int next_best_gScore = 999999;

                        foreach (NodeController n in current.ConnectedNodes)
                        {
                            if (n == next_best)
                                continue; //continue because next_best node is avoided
                            if (n == prev)
                                continue; // don't want to go back

                            if (gScore[n] < next_best_gScore && !spaceMap[i].ContainsKey(n))
                            {
                                next_best = n;
                                next_best_gScore = gScore[n];
                            }
                        }

                        if (next_best_gScore == 999999 && prev != null && !spaceMap[i].ContainsKey(prev))
                        {
                            next_best = prev;
                        }
                    }
                }

                prev = current;
                current = next_best;
                portionPath.Add(current);
                steps_left--;
                prevNode = prev;
                currentNode = current;
                continue;
            }

            ///------------------------------------------------------------------------------------------------------------

            // Case 2: Next best node is occupied. Find another node

            int current_bestgScore = gScore[current];
            int next_bestgScore = 999999;

            
            foreach(NodeController n in current.ConnectedNodes)
            {
                if (n == next_best)
                    continue;

                if (n == prev)
                    continue;

                if (gScore[n] < next_bestgScore && !spaceMap[i].ContainsKey(n))
                {
                    next_best = n;
                    next_bestgScore = gScore[n];
                }
            }

            // In case of stuck go back
            if (next_bestgScore == 999999 && prev != null && !spaceMap[i].ContainsKey(prev))
            {
                next_best = prev;
            }

            prev = current;
            current = next_best;
            portionPath.Add(current);
            steps_left--;

            prevNode = prev;
            currentNode = current;
        }

        while (steps_left > 0)
        {
            portionPath.Add(current);
            steps_left--;
        }
    }

    public void GetPortionPath(ref List<NodeController> p)
    {
        p.Clear();

        foreach (NodeController n in portionPath)
        {
            p.Add(n);
        }
    }

    /// <summary>
    /// Update a new a new goal dynamically
    /// </summary>
    public void UpdateGoal(NodeController new_goal)
    {
        goalPoint = new_goal;
    }

    private void CalculateNextStep()
    {
        List<Dictionary<NodeController, PlayerController>> spaceMap = new List<Dictionary<NodeController, PlayerController>>();
        SetPortionPath(ref spaceMap);
    }

    public void SetTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            reachedDest = false;
        }
        else
            reachedDest = true;
    }

    public bool ReachedDestination()
    {
        return reachedDest;
    }

    public bool ReachedGoal()
    {
        return (Vector3.Distance(transform.position, goalPoint.transform.position) <= agent.stoppingDistance);
    }

    public void MoveToTarget()
    {
        if (target == null)
            return;

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(new Vector3(UnityEngine.Random.Range(agent.desiredVelocity.x, 2 * agent.desiredVelocity.x),
                        UnityEngine.Random.Range(agent.desiredVelocity.y, 2 * agent.desiredVelocity.y),
                        UnityEngine.Random.Range(agent.desiredVelocity.z, 2 * agent.desiredVelocity.z))
                        , false, false);
        else
        {
            character.Move(Vector3.zero, false, false);
            reachedDest = true;
        }
    }
}

public class PriorityQueue
{
    List<NodeController> nodeList;

    public PriorityQueue()
    {
        nodeList = new List<NodeController>();
    }

    public int Count()
    {
        return nodeList.Count;
    }

    public bool Empty()
    {
        return nodeList.Count == 0;
    }

    public void Add(NodeController n)
    {
        nodeList.Add(n);

        HeapUp();
    }

    public NodeController Pop()
    {
        NodeController result;

        NodeController temp = nodeList[nodeList.Count - 1];
        nodeList[nodeList.Count-1] = nodeList[0];
        nodeList[0] = temp;
        result = nodeList[nodeList.Count - 1];

        nodeList.RemoveAt(nodeList.Count - 1);
        HeapDown();

        return result;
    }

    private void HeapUp()
    {
        int ind = nodeList.Count - 1;

        while (ind != 0)
        {
            int parent = (ind - 1) / 2;

            if (nodeList[parent].fScore > nodeList[ind].fScore)
            {
                NodeController temp = nodeList[parent];
                nodeList[parent] = nodeList[ind];
                nodeList[ind] = temp;
                ind = parent;
            }
            else
                break;
        }
    }

    private void HeapDown()
    {
        int ind = 0;

        while (true)
        {
            if (2 * ind + 1 >= nodeList.Count)
                break;

            int left_child = 2 * ind + 1;
            int right_child = 2 * ind + 2;

            if (right_child >= nodeList.Count)
                right_child = 2 * ind + 1;

            if (nodeList[ind].fScore > nodeList[left_child].fScore || nodeList[ind].fScore > nodeList[right_child].fScore)
            {
                if (nodeList[left_child].fScore < nodeList[right_child].fScore)
                {
                    NodeController temp = nodeList[left_child];
                    nodeList[left_child] = nodeList[ind];
                    nodeList[ind] = temp;
                    ind = left_child;
                }
                else
                {
                    NodeController temp = nodeList[right_child];
                    nodeList[right_child] = nodeList[ind];
                    nodeList[ind] = temp;
                    ind = right_child;
                }
            }
            else
                break;
        }
    }
}
