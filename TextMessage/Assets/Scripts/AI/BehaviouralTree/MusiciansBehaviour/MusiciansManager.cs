using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiciansManager : MonoBehaviour
{
    private List<GameObject> musicians;
    [SerializeField]
    private GameObject StagePerformanceArea;
    [SerializeField]
    private GameObject ExitPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        musicians = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckTurn(GameObject musician)
    {
        if (musicians[0] == musician)
            return true;

        return false;
    }

    public GameObject GetStagePerformanceArea()
    {
        return StagePerformanceArea;
    }

    public GameObject GetExitPoint()
    {
        return ExitPoint;
    }
}
