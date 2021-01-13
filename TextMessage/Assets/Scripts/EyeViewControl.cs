using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeViewControl : MonoBehaviour
{
    private PlayerAttackController _player;
    private AlienControl _alien;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerAttackController>();
        _alien = FindObjectOfType<AlienControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) >= 10f)
            return false;

        Vector3 playerPos = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        Vector3 dir = playerPos - transform.position;

        float angle = Vector3.AngleBetween(transform.forward, dir);

        if (angle > Mathf.PI / 6) // 30 deg
            return false;

        return true;
    }

    public bool CanSeeAlien()
    {
        if (Vector3.Distance(_alien.transform.position, transform.position) >= 10f)
            return false;

        Vector3 alienPos = new Vector3(_alien.transform.position.x, transform.position.y, _alien.transform.position.z);
        Vector3 dir = alienPos - transform.position;

        float angle = Vector3.AngleBetween(transform.forward, dir);

        if (angle > Mathf.PI / 6) // 30 deg
            return false;

        return true;
    }
}
