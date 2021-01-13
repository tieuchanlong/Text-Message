using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayControl : MonoBehaviour
{
    [SerializeField]
    private bool goLeft = false;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject stopLeft;
    [SerializeField]
    private GameObject stopRight;
    private bool waitForPassenger = false;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitForPassenger)
            Move();
        else
            _audio.Stop();
    }

    private void Move()
    {
        if (goLeft)
            transform.position -= speed * transform.right * Time.deltaTime;
        else
            transform.position += speed * transform.right * Time.deltaTime;
        if (!_audio.isPlaying)
            _audio.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            goLeft = !goLeft;
        }

        if (other.gameObject == stopLeft)
        {
            stopLeft.SetActive(false);
            stopRight.SetActive(true);
            StartCoroutine(WaitForPassenger());
        }

        if (other.gameObject == stopRight)
        {
            stopLeft.SetActive(true);
            stopRight.SetActive(false);
            StartCoroutine(WaitForPassenger());
        }
    }

    IEnumerator WaitForPassenger()
    {
        waitForPassenger = true;
        yield return new WaitForSeconds(5);
        waitForPassenger = false;
    }
}
