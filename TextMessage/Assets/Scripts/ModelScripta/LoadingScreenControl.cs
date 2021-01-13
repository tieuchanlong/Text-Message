using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreenControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI loadText;
    [SerializeField]
    private TextMeshProUGUI tipText;
    [SerializeField]
    private float phaseRate = 0.1f;
    [SerializeField]
    private float phaseAmount = 100f;
    [SerializeField]
    private List<string> tips;
    private bool canPhase = true;
    private bool phaseIn = false;

    private MainMenuControl _mainMenuControl;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuControl = FindObjectOfType<MainMenuControl>();
        loadText.color = new Color(loadText.color.r, loadText.color.g,
            loadText.color.b, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Phase()
    {
        if (canPhase)
            if (phaseIn)
                PhaseIn();
            else
                PhaseOut();
    }

    private void PhaseIn()
    {
        canPhase = false;
        loadText.color = new Color(loadText.color.r, loadText.color.g,
            loadText.color.b, loadText.color.a + phaseAmount / 255f);

        if (loadText.color.a >= 1.0f)
        {
            phaseIn = false;
        }

        //yield return new WaitForSeconds(phaseRate);
        canPhase = true;
    }

    private void PhaseOut()
    {
        canPhase = false;
        loadText.color = new Color(loadText.color.r, loadText.color.g,
            loadText.color.b, loadText.color.a - phaseAmount / 255f);

        if (loadText.color.a <= 0.0f)
        {
            phaseIn = true;
        }

        //yield return new WaitForSeconds(phaseRate);
        canPhase = true;
    }

    private void OnEnable()
    {
        tipText.text = tips[Random.Range(0, tips.Count)];
        canPhase = true;
    }

    private void OnDisable()
    {
        canPhase = false;
        phaseIn = true;
        loadText.color = new Color(loadText.color.r, loadText.color.g,
            loadText.color.b, 0);
    }
}
