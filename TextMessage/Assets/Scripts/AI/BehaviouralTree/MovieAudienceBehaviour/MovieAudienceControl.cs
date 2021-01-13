using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovieAudienceControl : MonoBehaviour
{
    private MovieAudienceNode _root;
    private MovieAudienceManager _movieAudienceManager;

    private NavMeshAgent _agent;

    private int currenPosInLine = -1;

    [HideInInspector]
    public bool topLine = false;
    [HideInInspector]
    public bool ticketCheckingFinished = false;
    [HideInInspector]
    public bool allowedIn = false;

    private GameObject designatedSeat;

    private int angryLevel;

    private float maxFightTime = 5f;
    private float currentFightTime = 0f;

    private bool startExiting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _movieAudienceManager = FindObjectOfType<MovieAudienceManager>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void PrepareBehaviourTree()
    {
        _root = new MovieAudienceNode(this);
        _root.AddNodes(new GoToLineNode(this));
        _root.AddNodes(new SelectorNode());
        (_root.GetNode(1) as SelectorNode).AddNodes(new SelectorNode());
        (_root.GetNode(1) as SelectorNode).AddNodes(new SelectorNode());
        (_root.GetNode(1) as SelectorNode).AddNodes(new ExitAreaNode(this));
        ((_root.GetNode(1) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new CanGoInCinemaNode(this));
        ((_root.GetNode(1) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new FindSeatNode(this));
        ((_root.GetNode(1) as SelectorNode).GetNode(1) as SelectorNode).AddNodes(new RandomizeAngryLevelNode(this));
        ((_root.GetNode(1) as SelectorNode).GetNode(1) as SelectorNode).AddNodes(new FightTicketerNode(this));
        ((_root.GetNode(1) as SelectorNode).GetNode(1) as SelectorNode).AddNodes(new ExitAreaNode(this));
    }

    public bool GoToLine()
    {
        if (currenPosInLine == -1)
        {
            currenPosInLine = _movieAudienceManager.GetAvailableWaitSpot();

            if (currenPosInLine != -1)
                _agent.SetDestination(_movieAudienceManager.GetWaitPoint(currenPosInLine).transform.position);
        }

        if (currenPosInLine != -1)
        {
            if (Vector3.Distance(transform.position,
                _movieAudienceManager.GetWaitPoint(currenPosInLine).transform.position) >= 2f)
                return false;
            else
            {
                _movieAudienceManager.LeaveWaitSpot(currenPosInLine);
                if (currenPosInLine > 0)
                {
                    if (_movieAudienceManager.WaitSpotAvailable(currenPosInLine - 1))
                    {
                        currenPosInLine--;
                        _agent.SetDestination(_movieAudienceManager.GetWaitPoint(currenPosInLine).transform.position);
                    }

                    return false;
                }
                else
                {
                    topLine = true;
                    return true;
                }
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CanGoInCinema()
    {
        if (ticketCheckingFinished)
        {
            if (allowedIn)
                return 1;
            else
                return -1;
        }

        return 0;
    }

    public bool FindSeat()
    {
        if (designatedSeat == null)
        {
            designatedSeat = _movieAudienceManager.GetAvailableSeat();
            _agent.SetDestination(designatedSeat.transform.position);
        }

        if (designatedSeat != null)
        {
            if (Vector3.Distance(transform.position, designatedSeat.transform.position) >= 2f)
                return false;
            else
                return true;
        }

        return false;
    }

    public bool RandomizeAngryLevel()
    {
        angryLevel = UnityEngine.Random.Range(0, 10);

        if (angryLevel <= 4)
            return false;
        else
            return true;
    }

    public bool FightTicketer()
    {
        // PlayAnimation
        if (currentFightTime >= maxFightTime)
        {
            currentFightTime = 0f;
            return true;
        }
        else
        {
            currentFightTime += Time.deltaTime;
            return false;
        }
    }

    public bool ExitArea()
    {
        if (!startExiting)
        {
            startExiting = true;
            _agent.SetDestination(_movieAudienceManager.GetExitPoint().transform.position);
        }

        if (startExiting)
        {
            if (Vector3.Distance(transform.position, _movieAudienceManager.GetExitPoint().transform.position) >= 2f)
                return false;
            else
                return true;
        }

        return false;
    }
}
