using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketCheckerControl : MonoBehaviour
{
    private TicketCheckerNode _root;
    private MovieAudienceManager _movieAudienceManager;

    private float maxCheckingTicketTime = 5f;
    private float currentCheckingTicketTime = 0f;

    private float maxAllowInTime = 2f;
    private float currenAllowInTime = 0f;

    private float maxWarningTime = 10f;
    private float currentWarningTime = 0f;

    private float maxCallGuardTime = 5f;
    private float currentCallGuardTime = 0f;

    [SerializeField]
    private ScriptableObject ticket;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void PrepareBehaviourTree()
    {
        _root = new TicketCheckerNode(this);
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new SelectorNode());
        (_root.GetNode(0) as SelectorNode).AddNodes(new SeeTicketNode(this));
        (_root.GetNode(0) as SelectorNode).AddNodes(new AllowInCinemaNode(this));
        (_root.GetNode(1) as SelectorNode).AddNodes(new WarningNode(this));
        (_root.GetNode(1) as SelectorNode).AddNodes(new CallGuardNode(this));
    }

    public bool WaitForNextPerson()
    {
        if (_movieAudienceManager.GetFirstMovieAudience() == null || 
            !_movieAudienceManager.GetFirstMovieAudience().GetComponent<MovieAudienceControl>().topLine)
            return false;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SeeTicket()
    {
        if (currentCheckingTicketTime >= maxCheckingTicketTime)
        {
            currentCheckingTicketTime = 0f;

            // Play animation
            GameObject movieAudience = _movieAudienceManager.GetFirstMovieAudience();
            movieAudience.GetComponent<MovieAudienceControl>().allowedIn = 
                movieAudience.GetComponent<EquipmentControl>().ContainEquipment(ticket);

            if (movieAudience.GetComponent<MovieAudienceControl>().allowedIn)
                return 1;
            else
                return -1;
        }
        else
            currentCheckingTicketTime += Time.deltaTime;

        return 0;
    }

    public bool AllowIn()
    {
        // Play Animation
        if (currenAllowInTime >= maxAllowInTime)
        {
            currenAllowInTime = 0f;
            return true;
        }
        else
            currenAllowInTime += Time.deltaTime;

        return false;
    }

    public bool Warning()
    {
        // Play Animation
        if (currentWarningTime >= maxWarningTime)
        {
            currentWarningTime = 0f;
            return true;
        }
        else
            currentWarningTime += Time.deltaTime;

        return false;
    }

    public bool CallGuard()
    {
        // Play Animation
        if (currentCallGuardTime >= maxCallGuardTime)
        {
            currentCallGuardTime = 0f;
            return true;
        }
        else
            currentCallGuardTime += Time.deltaTime;

        return false;
    }
}
