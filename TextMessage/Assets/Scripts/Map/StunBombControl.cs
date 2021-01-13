using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBombControl : MonoBehaviour
{
    private PlayerStateControl _playerStateControl;

    private bool startBombSound = false;
    private AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
        _playerStateControl = FindObjectOfType<PlayerStateControl>();
        //_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Activate();
    }

    private void Activate()
    {
        if (Vector3.Distance(transform.position, _playerStateControl.transform.position) <= 20f)
        {
            if (!startBombSound)
                // Activate the bomb and stun player
                StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        startBombSound = true;
        // Play SOund
        //_audio.Play();
        Debug.Log("Start to explode");
        yield return new WaitForSeconds(3);
        startBombSound = false;
        // Explode bomb
        //_audio.Stop();
        Debug.Log("Explode");
        _playerStateControl.ChangeState(PlayerStateControl.State.Stun);
    }
}
