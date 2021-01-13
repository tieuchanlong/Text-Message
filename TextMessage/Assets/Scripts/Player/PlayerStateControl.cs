using Cinemachine.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateControl : MonoBehaviour
{
    public enum State
    {
        Normal, 
        Stun,
        Observe
    }

    private State state;

    private float maxStunTime = 10f;
    private float currentStunTime = 0f;

    [SerializeField]
    private GameObject mainCamera;
    
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
                ActivatePlayerControls(true);
                mainCamera.SetActive(true);
                break;
            case State.Stun:
                if (currentStunTime >= maxStunTime)
                {
                    currentStunTime = 0;
                    state = State.Normal;
                }

                currentStunTime += Time.deltaTime;

                ActivatePlayerControls(false);
                break;
            case State.Observe:
                GetComponent<CharacterMovement>().enabled = false;
                mainCamera.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void ActivatePlayerControls(bool on)
    {
        GetComponent<PlayerAttackController>().enabled = on;
        GetComponent<CharacterMovement>().enabled = on;
        GetComponent<PlayerSneakAttackControl>().enabled = on;
    }

    public void ChangeState(State new_state)
    {
        state = new_state;
    }
}
