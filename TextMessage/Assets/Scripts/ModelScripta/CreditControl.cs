using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditControl : MonoBehaviour
{
    [SerializeField]
    private List<string> credits;
    [SerializeField]
    private TextMeshProUGUI creditText;
    [SerializeField]
    private float phaseRate = 0.1f;
    [SerializeField]
    private float phaseAmount = 10f;
    private bool canPhase = true;
    private bool phaseIn = true;
    private int currentCreditInd = 0;

    private MainMenuControl _mainMenuControl;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuControl = FindObjectOfType<MainMenuControl>();
        creditText.color = new Color(creditText.color.r, creditText.color.g,
            creditText.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCreditInd >= credits.Count)
        {
            currentCreditInd = 0;
            canPhase = false;
            creditText.color = new Color(creditText.color.r, creditText.color.g,
            creditText.color.b, 0f);
            _mainMenuControl.CloseCredit();
        }

        if (canPhase)
            if (phaseIn)
                StartCoroutine(PhaseIn());
            else
                StartCoroutine(PhaseOut());
    }

    IEnumerator PhaseIn()
    {
        canPhase = false;
        creditText.text = credits[currentCreditInd];
        creditText.color = new Color(creditText.color.r, creditText.color.g, 
            creditText.color.b, creditText.color.a + phaseAmount / 255f);

        if (creditText.color.a >= 1.0f)
        {
            phaseIn = false;
        }

        yield return new WaitForSeconds(phaseRate);
        canPhase = true;
    }

    IEnumerator PhaseOut()
    {
        canPhase = false;
        creditText.text = credits[currentCreditInd];
        creditText.color = new Color(creditText.color.r, creditText.color.g,
            creditText.color.b, creditText.color.a - phaseAmount / 255f);

        if (creditText.color.a <= 0.0f)
        {
            phaseIn = true;
            currentCreditInd++;
        }

        yield return new WaitForSeconds(phaseRate);
        canPhase = true;
    }

    private void OnEnable()
    {
        canPhase = true;
    }

    private void OnDisable()
    {
        currentCreditInd = 0;
        canPhase = false;
        phaseIn = true;
        creditText.color = new Color(creditText.color.r, creditText.color.g,
            creditText.color.b, 0);
    }
}
