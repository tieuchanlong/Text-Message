using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LghtMovement : MonoBehaviour
{
    private bool goCounterClockwise = true;
    private float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (goCounterClockwise)
            transform.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
        else
            transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));

        if (transform.rotation.y < 30f/180f * Mathf.PI || transform.rotation.y > 80f/180f * Mathf.PI)
            goCounterClockwise = !goCounterClockwise;
    }
}
