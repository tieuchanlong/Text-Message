using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Door;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int maxTravel;

    private int currentTravel;

    private bool canTravel = false;

    private bool openDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        maxTravel = currentTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTravel == maxTravel)
        {
            canTravel = false;

            Door.SetActive(!openDoor);
        }

        if (canTravel)
            currentTravel++;
    }

    public void OpenDoor()
    {

        canTravel = true;
        currentTravel = 0;
        openDoor = true;
    }

    public void CloseDoor()
    {
        canTravel = true;
        currentTravel = 0;
        openDoor = false;
    }
}
