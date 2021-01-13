using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBlurEffect : MonoBehaviour
{
    [SerializeField]
    private Shader shader;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(shader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, mat);
    }
}
