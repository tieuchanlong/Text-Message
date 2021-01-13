using Cinemachine.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStateControl : MonoBehaviour
{
    public enum State
    {
        Normal,
        Dead,
        TakeHostage,
        Kill
    }

    private State state;
    private bool spawnBlood = false;

    [SerializeField]
    private GameObject bloodPrefab;
    
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
                GetComponent<CharacterMovement>().enabled = true;
                break;
            case State.TakeHostage:
                GetComponent<CharacterMovement>().enabled = false;
                break;
            case State.Kill:
                GetComponent<CharacterMovement>().enabled = true;
                break;
            default:
                break;
        }
    }

    public void ChangeState(State new_state)
    {
        state = new_state;
    }

    IEnumerator SpawnBlood()
    {
        spawnBlood = true;
        yield return new WaitForSeconds(2);
        // SPawn blood
        spawnBlood = false;
    }
}
