using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitersManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> WaitPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetWaitPoint()
    {
        GameObject waitPoint = WaitPoints[Random.Range(0, WaitPoints.Count)];
        WaitPoints.Remove(waitPoint);

        return waitPoint;
    }

    public void LeaveWaitPoint(GameObject waitPoint)
    {
        WaitPoints.Add(waitPoint);
    }
}
