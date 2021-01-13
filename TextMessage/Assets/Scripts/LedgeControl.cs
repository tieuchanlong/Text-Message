using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeControl : MonoBehaviour
{
    [SerializeField]
    private bool drawRaycasts;
    [SerializeField]
    private float fowardRate = 0.1f;
    [SerializeField]
    private float handBuffer = 1f;
    private Vector3 handPosition;

    public bool hangState = false;
    private Rigidbody _rigidBody;
    public GameObject Ledge;
    
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = transform.parent.position + 2*transform.up;
        _rigidBody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        handPosition = transform.position + fowardRate * transform.forward;

        //if (_rigidBody.velocity.y < 0)
           //if (!hangState)
                //hangState = GizmoRaycast(handPosition, -transform.up, out hit, handBuffer, LayerMask.GetMask("Ledge"));
    }

    bool GizmoRaycast(Vector3 start, Vector3 direction, out RaycastHit hit, float distance, LayerMask layermask, Color hitColor, Color unhitColor, float time = 0)
    {
        var didHit = Physics.Raycast(start, direction, out hit, distance, layermask);
        if (drawRaycasts)
        {
            DebugExtension.DebugArrow(start, direction * (didHit ? hit.distance : distance), didHit ? hitColor : unhitColor, time);
        }
        return didHit;
    }

    bool GizmoRaycast(Vector3 start, Vector3 direction, out RaycastHit hit, float distance, LayerMask layermask, float time = 0)
    {
        return GizmoRaycast(start, direction, out hit, distance, layermask, Color.red, Color.green, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge") && _rigidBody.velocity.y < 0)
        {
            hangState = true;
            Ledge = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ledge") && _rigidBody.velocity.y > 0)
        {
            hangState = false;
        }
    }
}
