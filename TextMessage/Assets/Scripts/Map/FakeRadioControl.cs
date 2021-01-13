using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRadioControl : MonoBehaviour
{
    private bool startMakeSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!startMakeSound)
            StartCoroutine(MakeSignalSound());
    }

    IEnumerator MakeSignalSound()
    {
        startMakeSound = true;
        yield return new WaitForSeconds(2);
        startMakeSound = false;
    }
}
