using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatingGuestControl : MonoBehaviour
{
    private EatingGuestBehaviourNode _root;
    private GameObject designatedSeat;
    private EatingGuestsManager _eatingGuestsManager;

    private NavMeshAgent _agent;

    private float maxEatingTime = 20f;
    private float currentEatingTime = 0f;

    private bool leaveArea = false;


    // Start is called before the first frame update
    void Start()
    {
        _eatingGuestsManager = FindObjectOfType<EatingGuestsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool EnjoyFood()
    {
        if (currentEatingTime >= maxEatingTime)
        {
            currentEatingTime = 0f;
            return true;
        }
        else
        {
            currentEatingTime += Time.deltaTime;
            return false;
        }
    }

    private void PrepareBehaviorTree()
    {
        _root = new EatingGuestBehaviourNode(this);
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new OrderFoodNode(this));
        _root.AddNodes(new EnjoyFoodNode(this));
        _root.AddNodes(new LeaveEatingAreaNode(this));
        (_root.GetNode(0) as SelectorNode).AddNodes(new SearchTableNode(this));
        (_root.GetNode(0) as SelectorNode).AddNodes(new GetToTableNode(this));
    }

    public bool SearchTable()
    {
        return _eatingGuestsManager.SeatAvailable();
    }

    public bool GetToTable()
    {
        if (designatedSeat == null)
        {
            designatedSeat = _eatingGuestsManager.GetAvailableSeat();
            _agent.SetDestination(designatedSeat.transform.position);
        }

        if (Vector3.Distance(transform.position, designatedSeat.transform.position) >= 2f)
            return false;
        else
        {
            
            return true;
        }
    }

    public bool OrderFood()
    {
        _eatingGuestsManager.OrderFoodNode(gameObject);

        return true;
    }

    public bool ExitArea()
    {
        if (!leaveArea)
        {
            _agent.SetDestination(_eatingGuestsManager.GetExitPoint().transform.position);
            leaveArea = true;
        }

        if (Vector3.Distance(transform.position, _eatingGuestsManager.GetExitPoint().transform.position) >= 2f)
            return false;
        else
        {
            _eatingGuestsManager.LeaveSeat(designatedSeat);
            designatedSeat = null;
            leaveArea = false;
            return true;
        }
    }
}
