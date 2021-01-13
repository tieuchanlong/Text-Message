using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MusicianControl : MonoBehaviour
{
    private MusicianBehaviourNode _root;
    private MusiciansManager _musiciansManager;

    private GameObject stagePerformanceArea;
    private NavMeshAgent _agent;

    private bool comeOnStage = false;

    private float maxPlayTime = 10f;
    private float currentPlayTime = 0f;

    private bool exitStage = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _musiciansManager = FindObjectOfType<MusiciansManager>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void PrepareBehaviorTree()
    {
        _root = new MusicianBehaviourNode(this);
        _root.AddNodes(new SelectorNode());
        _root.AddNodes(new LeaveStageNode(this));
        (_root.GetNode(0) as SelectorNode).AddNodes(new SelectorNode());
        (_root.GetNode(0) as SelectorNode).AddNodes(new PlayMusicNode(this));
        ((_root.GetNode(0) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new CheckMusicianTurnNode(this));
        ((_root.GetNode(0) as SelectorNode).GetNode(0) as SelectorNode).AddNodes(new ComeOnStageNode(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckTurn()
    {
        return _musiciansManager.CheckTurn(gameObject);
    }

    public bool LeaveStage()
    {
        if (!exitStage)
        {
            _agent.SetDestination(_musiciansManager.GetExitPoint().transform.position);
            exitStage = true;
        }

        if (Vector3.Distance(transform.position, _musiciansManager.GetExitPoint().transform.position) >= 2f)
            return false;
        else
        {
            exitStage = false;
            return true;
        }
    }

    public bool ComeOnStage()
    {
        if (!comeOnStage)
        {
            _agent.SetDestination(_musiciansManager.GetStagePerformanceArea().transform.position);
            comeOnStage = true;
        }

        if (Vector3.Distance(transform.position, _musiciansManager.GetStagePerformanceArea().transform.position) >= 2f)
            return false;
        else
        {
            comeOnStage = false;
            return true;
        }
    }

    public bool PlayMusic()
    {
        if (currentPlayTime >= maxPlayTime)
        {
            currentPlayTime = 0f;
            return true;
        }
        else
            currentPlayTime += Time.deltaTime;

        return false;
    }
}
