using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewersManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> interestPoints;
    private List<GameObject> chosenInterestPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ChooseInterestPoint()
    {
        GameObject interestPoint = interestPoints[Random.Range(0, interestPoints.Count)];

        int rand = Random.Range(0, 10);

        if (rand > 7 && chosenInterestPoints.Count > 0)
        {
            interestPoint = chosenInterestPoints[Random.Range(0, chosenInterestPoints.Count)];
            return interestPoint;
        }

        interestPoints.Remove(interestPoint);
        chosenInterestPoints.Add(interestPoint);
        return interestPoint;
    }

    public void LeaveInterestPoint(GameObject interestPoint)
    {
        interestPoints.Add(interestPoint);
        chosenInterestPoints.Remove(interestPoint);
    }

    public bool InterestPointChosen(GameObject interestPoint)
    {
        return chosenInterestPoints.Contains(interestPoint);
    }
}
