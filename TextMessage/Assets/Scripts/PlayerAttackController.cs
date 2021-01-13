using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator _animator;
    private bool walking;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    [SerializeField]
    private GameObject gunHole;
    [SerializeField]
    private GameObject bulletPrefab;
    private LedgeControl _ledgeControl;
    private Rigidbody _rigidBody;

    private bool climbing = false;
    private bool check = false;
    private bool startClimbing = false;
    private bool finishedClimbing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _ledgeControl = GetComponentInChildren<LedgeControl>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        if (Input.GetKeyDown(KeyCode.E) && _ledgeControl.hangState)
            climbing = true;

        if (CheckAiming())
        {
            _animator.SetBool("Aim", true);
            if (CheckWalking())
                _animator.SetBool("AimWalking", true);
            else
                _animator.SetBool("AimWalking", false);
        }
        else
        {
            _animator.SetBool("Aim", false);
            _animator.SetBool("AimWalking", false);
        }

        if (_rigidBody.velocity.y < -0.5f && !climbing && _ledgeControl.hangState)
        {
            _animator.SetBool("Hang", _ledgeControl.hangState);
            _rigidBody.useGravity = !_ledgeControl.hangState;
            finishedClimbing = false;
        }

        if (climbing)
        {
            if (!check)
            {
                _animator.SetBool("Climb", true);
                _animator.SetBool("Hang", false);
                check = true;
                _ledgeControl.hangState = false;
            }
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Climbing"))
        {
            _animator.SetBool("Climb", false);
            startClimbing = true;
        }
        else if (startClimbing)
        {
            _rigidBody.useGravity = true;

            if (_ledgeControl.Ledge != null && climbing && !finishedClimbing)
            {
                transform.position = _ledgeControl.Ledge.transform.position + 0.2f * _ledgeControl.Ledge.transform.up;
                finishedClimbing = true;
                climbing = false;
                startClimbing = false;
                check = false;
            }
        }



        Shoot();
    }

    private bool CheckWalking()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
    }

    private bool CheckAiming()
    {
        return (Input.GetMouseButton(1));
    }

    private void Jump()
    {
        if (_rigidBody.velocity.y == 0)
            if (Input.GetKeyDown(KeyCode.Space))
                _rigidBody.AddForce(transform.up * 7f, ForceMode.Impulse);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && CheckAiming())
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.localScale = 0.1f * bulletPrefab.transform.localScale;
            bullet.transform.position = gunHole.transform.position;
            bullet.transform.forward = gunHole.transform.forward;
            //_animator.SetBool("Shoot", true);
        }
        else
        {
            //_animator.SetBool("Shoot", false);
        }
    }
}
