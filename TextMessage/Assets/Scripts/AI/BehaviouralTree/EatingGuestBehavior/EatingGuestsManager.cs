using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingGuestsManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Seats;
    [SerializeField]
    private GameObject ExitPoint;

    private List<GameObject> GuestOrders;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SeatAvailable()
    {
        return Seats.Count > 0;
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

    public void OrderFoodNode(GameObject guest)
    {
        GuestOrders.Add(guest);
    }

    public bool OrdersAvailable()
    {
        return GuestOrders.Count > 0;
    }

    public GameObject GetOrder()
    {
        if (GuestOrders.Count == 0)
            return null;

        GameObject guest = GuestOrders[0];
        GuestOrders.Remove(guest);

        return guest;
    }

    public GameObject GetExitPoint()
    {
        return ExitPoint;
    }
}
