using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconControl : MonoBehaviour
{
    [SerializeField]
    private bool isAlien = false;

    private PlayerRadarControl _playerRadarControl;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRadarControl = FindObjectOfType<PlayerRadarControl>();
        PrepareIcon();
    }

    private void PrepareIcon()
    {
        _playerRadarControl.AddNewIcon(this, isAlien);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _playerRadarControl.RemoveIcon(this);
    }
}
