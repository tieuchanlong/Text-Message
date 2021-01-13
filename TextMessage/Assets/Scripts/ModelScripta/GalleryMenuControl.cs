using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryMenuControl : MonoBehaviour
{
    [SerializeField]
    private GameObject GalleryMenu;
    [SerializeField]
    private GameObject PrevButton;
    [SerializeField]
    private GameObject NextButton;
    [SerializeField]
    private float moveDistance = 800f;
    [SerializeField]
    private float moveSpeed = 2f;
    private int moving = -1;
    private float currentMovingDist = 0f;
    private int currentImgIndex = 0;
    [SerializeField]
    private AudioSource ButtonSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == 0)
        {
            GalleryMenu.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(GalleryMenu.GetComponent<RectTransform>().anchoredPosition.x - moveSpeed * Time.deltaTime,
            GalleryMenu.GetComponent<RectTransform>().anchoredPosition.y);
            currentMovingDist += moveSpeed * Time.deltaTime;
        }
        else if (moving == 1)
        {
            GalleryMenu.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(GalleryMenu.GetComponent<RectTransform>().anchoredPosition.x + moveSpeed * Time.deltaTime,
            GalleryMenu.GetComponent<RectTransform>().anchoredPosition.y);
            currentMovingDist += moveSpeed * Time.deltaTime;
        }

        if (currentMovingDist >= moveDistance)
        {
            currentMovingDist = 0;
            moving = -1;
        }

        PrevButton.SetActive(currentImgIndex > 0);
        NextButton.SetActive(currentImgIndex < GalleryMenu.transform.childCount - 1);
    }

    public void MoveToNextGalleryImage()
    {
        if (moving == -1 && currentImgIndex < GalleryMenu.transform.childCount - 1)
        {
            moving = 0;
            ButtonSound.Play();
            currentImgIndex++;
        }

    }

    public void MoveToPrevGalleryImage()
    {
        if (moving == -1 && currentImgIndex > 0)
        {
            moving = 1;
            ButtonSound.Play();
            currentImgIndex--;
        }
    }
}
