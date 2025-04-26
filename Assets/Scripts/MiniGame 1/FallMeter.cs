using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class FallMeter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private List<TMP_Text> panelBarsText;
    [SerializeField] private List<FreeFallCharacter> freeFallCharacters;

    [Header("Camera & Scoreboard")]
    [SerializeField] private List<GameObject> playerCameraObjects;
    [SerializeField] private GameObject scoreBoardCamera;
    [SerializeField] private GameObject scoreBoardObject;

    private const float startY = 2352f;
    private List<float> lastKnownDistances;
    private bool scoreboardShown = false;

    private void Awake()
    {
        lastKnownDistances = new List<float>(new float[freeFallCharacters.Count]);
    }

    private void Update()
    {
        if (panelBarsText.Count != freeFallCharacters.Count)
            return;

        for (int i = 0; i < freeFallCharacters.Count; i++)
        {
            if (freeFallCharacters[i] != null)
            {
                float playerPosY = freeFallCharacters[i].transform.position.y;
                float distanceFallen = Mathf.Max(0f, startY - playerPosY);
                lastKnownDistances[i] = distanceFallen;
                panelBarsText[i].text = $"{Mathf.FloorToInt(distanceFallen)}m";
            }
            else
            {
                panelBarsText[i].text = $"{Mathf.FloorToInt(lastKnownDistances[i])}m";
            }
        }

        if (!scoreboardShown && AllPlayerCamerasInactive())
        {
            ShowScoreboard();
        }
    }

    private bool AllPlayerCamerasInactive()
    {
        foreach (GameObject camObj in playerCameraObjects)
        {
            if (camObj != null && camObj.activeInHierarchy)
                return false;
        }
        return true;
    }

    private void ShowScoreboard()
    {
        if (scoreBoardCamera != null)
            scoreBoardCamera.SetActive(true);

        if (scoreBoardObject != null)
            scoreBoardObject.SetActive(true);

        scoreboardShown = true;
    }

    public List<(int playerIndex, float distance)> GetSortedFallResults() // This method is for ScoreBoard to order it.
    {
        return lastKnownDistances
            .Select((distance, index) => (playerIndex: index, distance))
            .OrderByDescending(p => p.distance)
            .ToList();
    }
}
