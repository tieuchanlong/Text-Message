using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelControl : MonoBehaviour
{
    [SerializeField]
    private float fadeRate = 0.2f;
    [SerializeField]
    private float fadeAmount = 10f;
    private bool canFade = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canFade)
            StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        canFade = false;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
            GetComponent<Image>().color.b, GetComponent<Image>().color.a - fadeAmount / 255f * Time.deltaTime);

        if (GetComponent<Image>().color.a >= 1.0f)
        {
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(fadeRate);
        canFade = true;
    }

    private void OnEnable()
    {
        canFade = true;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
            GetComponent<Image>().color.b, 1f);
    }

    private void OnDisable()
    {
        canFade = false;
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
            GetComponent<Image>().color.b, 0f);
    }
}
