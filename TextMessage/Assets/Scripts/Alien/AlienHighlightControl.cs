using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHighlightControl : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAlienInRange())
        {
            // Highlight the alien
        }
    }

    private bool CheckAlienInRange()
    {
        HeatSpiderControl[] heatSpiderControls = FindObjectsOfType<HeatSpiderControl>();

        foreach (HeatSpiderControl heatSpiderControl in heatSpiderControls)
        {
            if (Vector3.Distance(transform.position, heatSpiderControl.transform.position) <= heatSpiderControl.detectRange && heatSpiderControl.active)
            {
                return true;
            }
        }

        return false;
    }
}
