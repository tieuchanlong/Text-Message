using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyEffect : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    [SerializeField]
    private Shader _shader;

    private void Awake()
    {
        if (_material == null)
            _material = new Material(_shader);   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, _material);
    }
}
