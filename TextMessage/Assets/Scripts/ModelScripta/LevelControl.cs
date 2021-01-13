using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    [SerializeField]
    private GameObject StartLevelPanel;
    [SerializeField]
    private int sceneNumber;


    // Start is called before the first frame update
    void Start()
    {
        StartLevelPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ExitLevel();
    }

    /// <summary>
    /// Return to the main menu
    /// </summary>
    private void ExitLevel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadSceneManager.instance.LoadScene(sceneNumber, 1);
        }
    }
}
