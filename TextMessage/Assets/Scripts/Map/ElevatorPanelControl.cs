using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanelControl : MonoBehaviour
{
    private int dir = -1;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int maxTravel;

    private bool movePanel;

    private int currentTravel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTravel == maxTravel && movePanel)
        {
            movePanel = false;
        }

        if (movePanel)
        {
            transform.position += speed * Time.deltaTime * dir * transform.up;
            currentTravel++;
        }
    }

    public void Move()
    {
        if (!movePanel)
        {
            movePanel = true;
            dir = -dir;
        }
    }
}
