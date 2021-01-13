using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneakAttackControl : MonoBehaviour
{
    [SerializeField]
    private float maxSeeDist = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSneakAttack();
    }

    private bool CheckSneakAttack()
    {
        RaycastHit hit;
        int layerMask = 1 << 12;

        if (Physics.Raycast(transform.position + transform.up, transform.TransformDirection(Vector3.forward), out hit, maxSeeDist, layerMask))
        {
            Debug.DrawRay(transform.position + transform.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            EyeViewControl eyeViewControl = hit.collider.gameObject.GetComponent<EyeViewControl>();
            if (eyeViewControl == null)
            {
                Debug.Log("No eye view");
                return false;
            }


            if (!eyeViewControl.CanSeePlayer())
                return true;
            else
                return false;
        }
        else
        {
            Debug.DrawRay(transform.position + transform.up, transform.TransformDirection(Vector3.forward) * maxSeeDist, Color.white);
            Debug.Log("Did not Hit");
            return false;
        }
    }
}
