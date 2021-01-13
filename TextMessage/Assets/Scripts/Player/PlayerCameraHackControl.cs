using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHackControl : MonoBehaviour
{
    private GameObject target;

    private bool startHacking = false;

    [SerializeField]
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (IsCameraInRange())
        {
            if (Input.GetKey(KeyCode.E) && !startHacking)
            {
                // 
                StartCoroutine(HackCamera());
            }
        }
    }

    private bool IsCameraInRange()
    {
        RaycastHit hit;
        int layerMask = 1 << 13;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            target = hit.collider.gameObject;
            target = target.transform.GetChild(0).gameObject;

            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }

    IEnumerator HackCamera()
    {
        startHacking = true;
        yield return new WaitForSeconds(10);
        startHacking = false;
        target.GetComponent<Camera>().enabled = true;
        target.GetComponent<PlayerCameraHackControl>().enabled = true;
        GetComponent<Camera>().enabled = false;
        GetComponent<PlayerCameraHackControl>().enabled = false;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.W))
            transform.Rotate(-speed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.S))
            transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
