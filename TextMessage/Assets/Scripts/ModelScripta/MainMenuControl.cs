using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject GalleryMenu;
    [SerializeField]
    private GameObject CreditMenu;
    [SerializeField]
    private GameObject ControlMenu;
    [SerializeField]
    private Volume _volume;
    private HighLightText[] highLightTexts;

    // Start is called before the first frame update
    void Start()
    {
        highLightTexts = FindObjectsOfType<HighLightText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GalleryMenu.activeSelf)
            CloseGalleryMenu();

        if (Input.GetKeyDown(KeyCode.Escape) && CreditMenu.activeSelf)
            CloseCredit();

        if (Input.GetKeyDown(KeyCode.Escape) && ControlMenu.activeSelf)
            CloseControlMenu();
    }

    public void OpenGalleryMenu()
    {
        GalleryMenu.SetActive(true);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(false);
        _volume.gameObject.SetActive(true);
    }

    private void CloseGalleryMenu()
    {
        GalleryMenu.SetActive(false);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(true);
        _volume.gameObject.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void OpenCredit()
    {
        CreditMenu.SetActive(true);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(false);
    }

    public void CloseCredit()
    {
        CreditMenu.SetActive(false);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(true);
    }

    public void OpenControlMenu()
    {
        ControlMenu.SetActive(true);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(false);
    }

    public void CloseControlMenu()
    {
        ControlMenu.SetActive(false);
        foreach (HighLightText highLightText in highLightTexts)
            highLightText.ControlHighLightText(true);
    }
}
