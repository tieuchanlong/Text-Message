using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private LoadingScreenControl loadingScreenControl;

    List<AsyncOperation> scenesLoading;

    private void Awake()
    {
        instance = this;
        scenesLoading = new List<AsyncOperation>();
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int current_ind, int ind)
    {
        scenesLoading.Add(SceneManager.UnloadSceneAsync(current_ind));
        scenesLoading.Add(SceneManager.LoadSceneAsync(ind, LoadSceneMode.Additive));
        loadingScreen.SetActive(true);
        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0;i < scenesLoading.Count;i++)
        {
            while (!scenesLoading[i].isDone)
            {
                loadingScreenControl.Phase();
                Debug.Log("Hell");
                yield return null;
                
            }
        }

        loadingScreen.SetActive(false);
    }
}
