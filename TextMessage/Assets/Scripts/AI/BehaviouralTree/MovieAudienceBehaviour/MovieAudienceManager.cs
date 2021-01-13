using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieAudienceManager : MonoBehaviour
{
    private List<GameObject> movieAudienceQueue;
    [SerializeField]
    private List<GameObject> waitPoints;
    private List<bool> occupiedWaitPoints;

    [SerializeField]
    private GameObject SeatsParent;
    private List<GameObject> Seats;

    [SerializeField]
    private GameObject ExitPoint;

    [SerializeField]
    private ScriptableObject ticket;
    
    // Start is called before the first frame update
    void Start()
    {
        movieAudienceQueue = new List<GameObject>();
        occupiedWaitPoints = new List<bool>(waitPoints.Count);

        Seats = new List<GameObject>();
        for (int i = 0;i < SeatsParent.transform.childCount; i++)
        {
            Seats.Add(SeatsParent.transform.GetChild(i).gameObject);
        }
    }

    private void SpawnMovieAudience()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetFirstMovieAudience()
    {
        if (movieAudienceQueue.Count > 0)
            return movieAudienceQueue[0];
        else
            return null;
    }

    public int GetAvailableWaitSpot()
    {
        int result = -1;

        for (int i = occupiedWaitPoints.Count - 1; i >= 0; i--)
            if (occupiedWaitPoints[i] == true)
                result = i + 1;

        if (result == -1)
            result = 0;
        else if (result >= occupiedWaitPoints.Count)
            return -1;

        occupiedWaitPoints[result] = true;

        return result;
    }

    public GameObject GetAvailableSeat()
    {
        GameObject seat = Seats[Random.Range(0, Seats.Count)];
        Seats.Remove(seat);

        return seat;
    }

    public void LeaveSeat(GameObject seat)
    {
        Seats.Add(seat);
    }

    public GameObject GetWaitPoint(int ind)
    {
        return waitPoints[ind];
    }

    public bool WaitSpotAvailable(int ind)
    {
        return occupiedWaitPoints[ind];
    }

    public void LeaveWaitSpot(int ind)
    {
        occupiedWaitPoints[ind] = false;
    }

    public GameObject GetExitPoint()
    {
        return ExitPoint;
    }
}
