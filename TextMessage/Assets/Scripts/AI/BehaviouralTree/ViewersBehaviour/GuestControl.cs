using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuestControl : MonoBehaviour
{
    private ViewersBehaviourNode _root;

    [SerializeField]
    private float maxLookHologramTime = 5f;
    private float currentLookHologramTime = 0f;

    [SerializeField]
    private float maxTalkingTime = 5f;
    private float currentTalkingTime = 0f;

    private NavMeshAgent _agent;
    private GameObject currentInterestPoint;

    private bool lookingAtHologram = false;

    private ViewersManager _viewersManager;

    // Start is called before the first frame update
    void Start()
    {
        _viewersManager = FindObjectOfType<ViewersManager>();
    }

    private void PrepareBehaviorTree()
    {
        _root = new ViewersBehaviourNode(this);
        _root.AddNodes(new MoveToPictureNode(this));
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new LookAtPicNode(this));
        (_root.GetNode(1) as SelectorNode).AddNodes(new HavePersonInterestedNode(this));
        (_root.GetNode(1) as SelectorNode).AddNodes(new TalkNode(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GoToPicPoint()
    {
        if (currentInterestPoint == null)
        {
            currentInterestPoint = _viewersManager.ChooseInterestPoint();
            _agent.SetDestination(currentInterestPoint.transform.position);
        }

        if (Vector3.Distance(transform.position, currentInterestPoint.transform.position) > 2.0f)
            return true;

        return false;
    }

    public bool LookAtPic()
    {
        if (currentLookHologramTime >= maxLookHologramTime)
        {
            currentLookHologramTime = 0;
            _viewersManager.LeaveInterestPoint(currentInterestPoint);
            currentInterestPoint = null;
            return true;
        }

        currentLookHologramTime += Time.deltaTime;

        return true;
    }

    public bool IsIterestedPersonClose()
    {
        if (_viewersManager.InterestPointChosen(currentInterestPoint))
        {
            // Find the person and play animation

            return true;
        }
        else
            return false;
    }

    public bool Talk()
    {
        // Play Talk Animation
        if (currentTalkingTime >= maxTalkingTime)
        {
            currentTalkingTime = 0f;
            return true;
        }

        currentTalkingTime += Time.deltaTime;

        return false;
    }
}
