using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlaceholderUI : MonoBehaviour
{
    private TopHead _topHead;
    [SerializeField]
    private Texture2D uiImage;
    [SerializeField]
    private GameObject uiPanel;

    // Start is called before the first frame update
    void Start()
    {
        _topHead = GetComponentInChildren<TopHead>();
        Material mat = new Material(Shader.Find("Diffuse"));
        mat.mainTexture = uiImage;
        uiPanel.GetComponent<MeshRenderer>().material = mat;
        transform.position = _topHead.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
