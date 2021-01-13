using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighLightText : MonoBehaviour
{
    private TextMeshPro notionText;
    private bool canHighLightText = true;
    private MainMenuControl _mainMenuControl;
    public enum ControlType
    {
        Gallery,
        Close,
        Credit,
        Controls,
        None
    }
    [SerializeField]
    private ControlType controlType;
    private bool onObject;

    [SerializeField]
    private AudioSource interactSound;

    // Start is called before the first frame update
    void Start()
    {
        notionText = GetComponentInChildren<TextMeshPro>();
        _mainMenuControl = FindObjectOfType<MainMenuControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canHighLightText)
            CheckMousePos();   
    }

    private void CheckMousePos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == gameObject.name)
            {
                if (notionText.gameObject.activeSelf == false)
                    interactSound.Play();

                notionText.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    switch (controlType) {
                        case ControlType.Gallery:
                            _mainMenuControl.OpenGalleryMenu();
                            break;
                        case ControlType.Close:
                            _mainMenuControl.QuitApplication();
                            break;
                        case ControlType.Credit:
                            _mainMenuControl.OpenCredit();
                            break;
                        case ControlType.Controls:
                            _mainMenuControl.OpenControlMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                notionText.gameObject.SetActive(false);
            }
        }
        else
        {
            notionText.gameObject.SetActive(false);
        }
    }

    public void ControlHighLightText(bool inp)
    {
        canHighLightText = inp;
    }
}
