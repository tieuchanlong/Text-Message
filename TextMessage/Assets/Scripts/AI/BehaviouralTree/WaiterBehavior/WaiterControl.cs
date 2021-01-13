using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterControl : MonoBehaviour
{
    private WaiterBehaviorNode _root;

    private EatingGuestsManager _eatingGuestsManager;
    private WaitersManager _waitersManager;

    private bool comeToTable = false;

    private NavMeshAgent _agent;
    private GameObject currentOrder;

    private float maxAskOrderTime = 5f;
    private float currentAskOrderTime = 0f;

    private float maxWaitTime = 10f;
    private float currentWaitTime = 0f;

    private GameObject designatedWaitPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        _eatingGuestsManager = FindObjectOfType<EatingGuestsManager>();
        _waitersManager = FindObjectOfType<WaitersManager>();
    }

    private void PrepareBehaviourTree()
    {
        _root = new WaiterBehaviorNode(this);
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new ComeToWaitPointNode(this));
        _root.AddNodes(new WaitForOrderNode(this));
        (_root.GetNode(0) as SelectorNode).AddNodes(new SelectorNode());
        (_root.GetNode(0) as SelectorNode).AddNodes(new AskForOrderNode(this));
        ((_root.GetNode(0) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new SeeOrderNode(this));
        ((_root.GetNode(0) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new ComeToTableNode(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AskOrder()
    {
        if (currentAskOrderTime >= maxAskOrderTime)
        {
            currentAskOrderTime = 0f;
            return true;
        }
        else
            currentAskOrderTime += Time.deltaTime;

        return false;
    }

    public bool ComeToTable()
    {
        if (!comeToTable)
        {
            _agent.SetDestination(currentOrder.transform.position);
            comeToTable = true;
        }

        if (Vector3.Distance(transform.position, currentOrder.transform.position) >= 2f)
            return false;
        else
        {
            comeToTable = false;
            currentOrder = null;
            return true;
        }
    }

    public bool SeeOrder()
    {
        currentOrder = _eatingGuestsManager.GetOrder();
        if (currentOrder != null) 
            return true;
        else
            return false;
    }

    public bool ComeToWaitPoint()
    {
        if (designatedWaitPoint == null)
        {
            designatedWaitPoint = _waitersManager.GetWaitPoint();
            _agent.SetDestination(designatedWaitPoint.transform.position);
        }

        if (Vector3.Distance(transform.position, designatedWaitPoint.transform.position) >= 2f)
            return false;
        else
            return true;
    }

    public bool WaitForOrder()
    {
        if (currentWaitTime >= maxWaitTime || _eatingGuestsManager.OrdersAvailable())
        {
            currentWaitTime = 0f;
            return true;
        }
        else
            currentWaitTime += Time.deltaTime;

        return false;
    }
}
