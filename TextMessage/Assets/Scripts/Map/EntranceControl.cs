using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceControl : MonoBehaviour
{
    [SerializeField]
    private GameObject entranceTeleportLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public void TeleportToLocation()
    {
        PlayerDoorControl playerDoorControl = FindObjectOfType<PlayerDoorControl>();
        playerDoorControl.transform.position = entranceTeleportLocation.transform.position;
    }
}
