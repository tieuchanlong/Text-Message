using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeatSpiderControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Spider;
    [SerializeField]
    private GameObject mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivateSpider()
    {
        Spider.SetActive(true);
        Spider.GetComponent<Camera>().enabled = true;
        mainCamera.SetActive(false);
    }
}
