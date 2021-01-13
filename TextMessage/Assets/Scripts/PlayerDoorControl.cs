using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutomaticDoor"))
        {
            other.GetComponent<AutomaticDoorControl>().OpenDoor();
        }

        if (other.CompareTag("ElevatorBase"))
        {
            other.GetComponent<ElevatorPanelControl>().Move();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                other.GetComponent<ElevatorControl>().OpenDoor();
        }

        if (other.CompareTag("Entrance"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                other.GetComponent<EntranceControl>().TeleportToLocation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AutomaticDoor"))
        {
            other.GetComponent<AutomaticDoorControl>().CloseDoor();
        }
    }
}
