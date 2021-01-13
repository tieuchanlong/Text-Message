using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateControl : MonoBehaviour
{
    public enum State
    {
        Normal,
        Dead,
        Trigger,
        Eaten
    }

    private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                break;
            case State.Dead:
                //Destroy(gameObject);
                break;
            case State.Eaten:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void ChangeState(State new_state)
    {
        state = new_state;
    }
}
