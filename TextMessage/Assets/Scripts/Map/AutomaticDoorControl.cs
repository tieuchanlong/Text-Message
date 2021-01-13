using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorControl : MonoBehaviour
{
    [SerializeField]
    private GameObject LeftDoor;

    [SerializeField]
    private GameObject RightDoor;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int maxTravel;

    public enum OpenDir
    {
        Both,
        Left,
        Right
    }

    [SerializeField]
    private OpenDir openDirection;

    private int currentTravel;

    private bool canTravel = false;

    private bool openDoor = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTravel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (((currentTravel == maxTravel && openDoor) 
            || (currentTravel == 0 && !openDoor)) && canTravel)
        {
            canTravel = false;
        }

        if (canTravel)
        {
            if (openDoor)
            {
                if (openDirection == OpenDir.Left)
                    LeftDoor.transform.position -= speed * Time.deltaTime * LeftDoor.transform.forward;

                if (openDirection == OpenDir.Right || openDirection == OpenDir.Both)
                    RightDoor.transform.position += speed * Time.deltaTime * LeftDoor.transform.forward;
            }
            else
            {
                if (openDirection == OpenDir.Left)
                    LeftDoor.transform.position += speed * Time.deltaTime * LeftDoor.transform.forward;

                if (openDirection == OpenDir.Right || openDirection == OpenDir.Both)
                    RightDoor.transform.position -= speed * Time.deltaTime * LeftDoor.transform.forward;
            }

            if (openDoor)
                currentTravel++;
            else
                currentTravel--;
        }
    }

    public void OpenDoor()
    {

        canTravel = true;
        openDoor = true;
    }

    public void CloseDoor()
    {
        canTravel = true;
        openDoor = false;
    }
}
