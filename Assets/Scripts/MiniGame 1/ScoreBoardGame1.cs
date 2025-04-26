using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreBoardGame1 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> resultPanels; 
    [SerializeField] private FallMeter fallMeterRef;                 
    [SerializeField] private GameObject fallbackCamera; // Scoreboard camera Display
    [SerializeField] private GameObject previousObjectToDisable;      
    [SerializeField] private float startY = 2352f;                    

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (previousObjectToDisable != null)
            previousObjectToDisable.SetActive(false);

        if (fallbackCamera != null)
            fallbackCamera.SetActive(true);

        DisplayFallResults();
    }

    private void DisplayFallResults()
    {
        var playerResults = fallMeterRef.GetSortedFallResults();

        for (int rank = 0; rank < playerResults.Count && rank < resultPanels.Count; rank++)
        {
            int playerNum = playerResults[rank].playerIndex + 1;
            int distance = Mathf.FloorToInt(playerResults[rank].distance);

            TMP_Text textComponent = resultPanels[rank].GetComponentInChildren<TMP_Text>(); // (is inside panelbarParent.)
            if (textComponent != null)
            {
                textComponent.text = $"{rank + 1}. Player {playerNum} - {distance} m";
            }

            resultPanels[rank].transform.SetSiblingIndex(rank);
        }
    }
}
