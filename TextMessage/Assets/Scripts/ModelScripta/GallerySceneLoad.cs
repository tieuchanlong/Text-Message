using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GallerySceneLoad : MonoBehaviour
{
    [SerializeField]
    private int sceneInd = 0;
    [SerializeField]
    private GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGalleryScene()
    {
        Canvas.SetActive(false);
        LoadSceneManager.instance.LoadScene(1, sceneInd);
        //SceneManager.LoadScene(sceneInd);
    }
}
