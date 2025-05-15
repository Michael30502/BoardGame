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
    [SerializeField] private GameObject colorRandomizer;

    private HashSet<int> winningPlayers = new HashSet<int>();


    private bool colorRandomizerEnabled = false;


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

        bool anyPlayerPast2200 = false;

        for (int i = 0; i < freeFallCharacters.Count; i++)
        {
            if (freeFallCharacters[i] != null)
            {
                float playerPosY = freeFallCharacters[i].transform.position.y;
                float distanceFallen = Mathf.Max(0f, startY - playerPosY);
                lastKnownDistances[i] = distanceFallen;
                panelBarsText[i].text = $"{Mathf.FloorToInt(distanceFallen)}m";

                if (distanceFallen >= 2200f)
                    anyPlayerPast2200 = true;
            }
            else
            {
                panelBarsText[i].text = $"{Mathf.FloorToInt(lastKnownDistances[i])}m";
            }
        }

        
        if (!colorRandomizerEnabled && anyPlayerPast2200)
        {
            if (colorRandomizer != null)
                colorRandomizer.SetActive(true);

            colorRandomizerEnabled = true;
        }

       
        if (!scoreboardShown && AllPlayerCamerasInactive())
        {
            ShowScoreboard();
        }
    }


    //For checking scoreboard should show basically.
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

    public void RegisterWin(int playerIndex)
    {
        if (!winningPlayers.Contains(playerIndex))
        {
            Debug.Log($"Player {playerIndex} WON!");
            winningPlayers.Add(playerIndex);
        }
    }


    public List<(int playerIndex, float distance)> GetSortedFallResults()
{
    return freeFallCharacters
        .Select((player, index) => (
            playerIndex: index,
            isWinner: player != null && player.HasWon,
            distance: lastKnownDistances[index]
        ))
        .OrderByDescending(p => p.isWinner)         // winner winner chicken dinner
        .ThenByDescending(p => p.distance)          
        .Select(p => (p.playerIndex, p.distance))   //Take here for Score Michael I think?.
        .ToList();
}

}
