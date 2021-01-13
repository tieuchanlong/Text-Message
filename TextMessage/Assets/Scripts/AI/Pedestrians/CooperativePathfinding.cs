using System.Collections;
using System.Collections.Generic;
using UMA;
using UnityEngine;

public class CooperativePathfinding : MonoBehaviour
{
    [SerializeField]
    private List<PlayerController> agents;

    private bool all_agents_find_goal = false;

    private int MAXMOVE = 8;

    private int phase = 0;

    private int current_step = 0;

    private List<Dictionary<NodeController, PlayerController>> spaceMap;

    [SerializeField]
    private List<GameObject> extraPlayers;

    private int extraSpawn = 0;

    private bool forcedUpdate = false; // In case when spawn more, don't let more ai move and recalculate path


    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerController agent in agents)
        {
            GetTrueHeuristicDistance(agent, agent.GetStart());
        }
    }

    private void GetTrueHeuristicDistance(PlayerController agent, NodeController requestNode)
    {
        bool goal_found = false;

        NodeController start = agent.GetGoal();
        NodeController goal = requestNode;

        agent.openSet.Add(start);
        agent.gScore.Add(start, 0);
        agent.fScore.Add(start, ManhattanHeuristic(start, goal));

        while (!agent.openSet.Empty())
        {
            NodeController current = agent.openSet.Pop();

            if (current == goal)
                goal_found = true;

            // Add to closed set
            agent.closedSet.Add(current, agent.fScore[current]);

            foreach(NodeController n in current.ConnectedNodes)
            {
                if (!agent.cameFrom.ContainsKey(n))
                {
                    agent.cameFrom.Add(n, current);

                    if (!agent.gScore.ContainsKey(n))
                    {
                        agent.gScore.Add(n, 999999);
                        agent.fScore.Add(n, 999999);
                    }
                }

                // Check if neighbor already explored
                if (agent.closedSet.ContainsKey(n))
                    continue;

                int dist_neighbor = 10;
                int tentative_gScore = agent.gScore[current] + dist_neighbor;
                if (tentative_gScore >= agent.gScore[n])
                    continue;

                agent.cameFrom[n] = current;
                agent.gScore[n] = tentative_gScore;

                if (!goal_found)
                {
                    agent.fScore[n] = tentative_gScore + ManhattanHeuristic(n, goal);// some dist
                    n.fScore = tentative_gScore + ManhattanHeuristic(n, goal); //. some dist
                }
                else
                {
                    int new_fScore = agent.fScore[current] = 20;
                    agent.fScore[n] = new_fScore;
                    n.fScore = new_fScore;
                }

                agent.openSet.Add(n);
            }
        }
    }

    public void TriggerSpawn()
    {
        extraSpawn++;
    }

    private void SpawnPedestrians()
    {
        if (extraSpawn > 0)
        {
            phase = 0;
            forcedUpdate = true;

            foreach (PlayerController agent in agents)
                agent.SetCurrentNode(agent.GetTarget());

        }

        while (extraSpawn > 0)
        {
            int rand_ind = Random.Range(0, extraPlayers.Count);

            GameObject agent = Instantiate(extraPlayers[rand_ind]) as GameObject;
            //agent.transform.position = extraPlayers[rand_ind].transform.position;
            agent.transform.localEulerAngles = extraPlayers[rand_ind].transform.localEulerAngles;
            agent.SetActive(true);
            agent.GetComponentInChildren<UMARandomAvatar>().BuildCharacter();
            agent.GetComponentInChildren<PlayerController>().SetStartAndGoalPoint();
            agents.Add(agent.GetComponentInChildren<PlayerController>());
            GetTrueHeuristicDistance(agent.GetComponentInChildren<PlayerController>(), 
                agent.GetComponentInChildren<PlayerController>().GetStart());
            extraSpawn--;
        }
    }

    private int ManhattanHeuristic(NodeController a, NodeController b)
    {
        return 10 * ((int)(Mathf.Abs(a.transform.position.x - b.transform.position.x) + Mathf.Abs(a.transform.position.z - b.transform.position.z)));
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 0)
            CalculatePathToTarget();
        else if (phase == 1)
            PrepareTarget(ref spaceMap);
        else if (phase == 2)
            MoveToTarget();

        SpawnPedestrians();
    }

    private void UpdateSpaceMap(ref List<Dictionary<NodeController, PlayerController>> spaceMap, PlayerController agent)
    {
        List<NodeController> l = new List<NodeController>();
        agent.GetPortionPath(ref l);

        for (int i = 0; i < MAXMOVE; i++)
        {
            if (l.Count == 0)
                return;
            if (!spaceMap[i].ContainsKey(l[0]))
                spaceMap[i].Add(l[0], agent);
            l.RemoveAt(0);
        }
    }

    private void CalculatePathToTarget()
    {
        RemovePlayerAtGoal();
        forcedUpdate = false;
        spaceMap = new List<Dictionary<NodeController, PlayerController>>();

        for (int i = 0; i < MAXMOVE; i++)
            spaceMap.Add(new Dictionary<NodeController, PlayerController>());

        // Check if agents reach goal
        int n_agents_at_goal = 0;
        foreach (PlayerController agent in agents)
            if (agent.GetCurrentNode() == agent.GetGoal())
                n_agents_at_goal++;

        // if reach goal then don't calculate anymore
        if (n_agents_at_goal == agents.Count)
        {
            all_agents_find_goal = true;

            foreach (PlayerController agent in agents)
            {
                List<NodeController> l = new List<NodeController>();
                agent.GetPortionPath(ref l);
                UpdateSpaceMap(ref spaceMap, agent);
            }

            return;
        }


        // if reach goal stop going

        foreach (PlayerController agent in agents)
        {
            agent.SetPortionPath(ref spaceMap);
            List<NodeController> l = new List<NodeController>();
            agent.GetPortionPath(ref l);
            UpdateSpaceMap(ref spaceMap, agent);
        }

        phase = 1;
    }

    private void PrepareTarget(ref List<Dictionary<NodeController, PlayerController>> spaceMap)
    {
        if (forcedUpdate)
        {
            phase = 0;
            current_step = 0;
            forcedUpdate = false;
            return;
        }

        // Update the current target to move
        foreach (KeyValuePair<NodeController, PlayerController> comp in spaceMap[current_step])
        {
            comp.Value.target = comp.Key;
        }

        current_step++;

        if (current_step < MAXMOVE)
            phase = 2;
        else
        {
            phase = 0;
            current_step = 0;
        }
    }

    private void RemovePlayerAtGoal()
    {
        for (int i = 0;i < agents.Count;i++)
            if (agents[i].reachedGoal)
            {
                Destroy(agents[i].gameObject);
                agents.RemoveAt(i);
                i--;
            }
    }

    private void MoveToTarget()
    {
        int n_agents_at_dest = 0;
        int n_agents_at_goal = 0;

        if (forcedUpdate)
        {
            phase = 0;
            current_step = 0;
            forcedUpdate = false;
            return;
        }

        foreach (PlayerController agent in agents)
        {
            agent.SetTarget();
            agent.MoveToTarget();

            if (agent.ReachedDestination())
            {
                n_agents_at_dest++;
                if (agent.GetTarget() == agent.GetGoal())
                {
                    agent.reachedGoal = true;
                    n_agents_at_goal++;
                }
            }


        }

        if (n_agents_at_dest == agents.Count)
        {
            phase = 1;

            if (all_agents_find_goal && n_agents_at_goal == agents.Count)
            {
                Debug.Log("Algorithm ended success");
                phase = -1;
            }
        }
    }
}

