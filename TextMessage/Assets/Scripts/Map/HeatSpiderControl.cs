using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSpiderControl : MonoBehaviour
{
    [HideInInspector]
    public bool active = false;
    public float detectRange = 5f;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float cameraSpeed = 20f;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject mainCamera;
    private PlayerStateControl _playerStateControl;

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        _playerStateControl = FindObjectOfType<PlayerStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MoveCamera();

        PlaceDown();
    }

    private void PlaceDown()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            camera.SetActive(false);
            _playerStateControl.ChangeState(PlayerStateControl.State.Normal);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }

    private void MoveCamera()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        camera.transform.Rotate(-vertical * Time.deltaTime * cameraSpeed, horizontal * Time.deltaTime * cameraSpeed, 0);
    }
}
