using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControl : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float maxSeeDist;

    private bool startEating;
    private bool startKilling;

    private AlienStateControl _alienStateControl;
    private bool holdTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        _alienStateControl = GetComponent<AlienStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!holdTarget)
            SneakAttack();
        else
        {
            //Eat target
            EatTarget();
            KillTarget();
        }
    }

    private void SneakAttack()
    {
        if (CanSneakAttack())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Hold the person
                target.transform.position = transform.position + transform.forward;
                _alienStateControl.ChangeState(AlienStateControl.State.TakeHostage);

                holdTarget = true;
            }
        }
    }

    private void EatTarget()
    {
        if (Input.GetKeyDown(KeyCode.E) && !startEating)
        {
            // transform
            StartCoroutine(EatHuman());
        }
    }

    IEnumerator EatHuman()
    {
        startEating = true;
        // Play Animation
        yield return new WaitForSeconds(10);
        target.GetComponent<NPCStateControl>().ChangeState(NPCStateControl.State.Eaten);
        startEating = false;
        holdTarget = false;
        _alienStateControl.ChangeState(AlienStateControl.State.Normal);
        //Stop animation
    }

    private void KillTarget()
    {
        if (Input.GetMouseButtonDown(0) && !startKilling)
        {
            StartCoroutine(KillHuman());
        }
    }

    IEnumerator KillHuman()
    {
        startKilling = true;
        // Play Animation
        yield return new WaitForSeconds(3);
        target.GetComponent<NPCStateControl>().ChangeState(NPCStateControl.State.Dead);
        startKilling = false;
        _alienStateControl.ChangeState(AlienStateControl.State.Normal);
        //Stop animation
    }

    private bool CanSneakAttack()
    {
        RaycastHit hit;
        int layerMask = 1 << 12;

        if (Physics.Raycast(transform.position + transform.up, transform.TransformDirection(Vector3.forward), out hit, maxSeeDist, layerMask))
        {
            Debug.DrawRay(transform.position + transform.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            target = hit.collider.gameObject;

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
