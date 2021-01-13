using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersSpawn : MonoBehaviour
{
    private CooperativePathfinding _cooperativePathfinding;
    private bool canSpawn = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _cooperativePathfinding = FindObjectOfType<CooperativePathfinding>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
            StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()
    {
        canSpawn = false;
        _cooperativePathfinding.TriggerSpawn();
        yield return new WaitForSeconds(2.0f);
        canSpawn = true;
    }
}
