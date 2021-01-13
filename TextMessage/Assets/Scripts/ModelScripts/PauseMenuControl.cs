using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    private bool pauseGame = false;
    private bool pressed = false;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private AudioSource OST;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            pressed = true;
            if (!pauseGame)
            {
                pauseGame = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                OST.Pause();
            }
            else
            {
                pauseGame = false;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                OST.UnPause();
            }
        }
        else
        {
            pressed = false;
        }
    }
}
