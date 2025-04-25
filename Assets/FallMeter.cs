using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FallMeter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<TMP_Text> panelBarsText;
    [SerializeField] private List<FreeFallCharacter> freeFallCharacters;

    private const float startY = 2352f;
    private List<float> lastKnownDistances;

    private void Awake()
    {
        lastKnownDistances = new List<float>(new float[freeFallCharacters.Count]);
    }

    private void Update()
    {
        if (panelBarsText.Count != freeFallCharacters.Count)
        {
            return;
        }

        for (int i = 0; i < freeFallCharacters.Count; i++)
        {
            if (freeFallCharacters[i] != null)
            {
                float playerPosY = freeFallCharacters[i].transform.position.y;
                float distanceFallen = Mathf.Max(0f, startY - playerPosY);

                lastKnownDistances[i] = distanceFallen; // inherited - Player Object
                //Converted to int to save a bit of update-performance while running for 4x panel.
                panelBarsText[i].text = $"{Mathf.FloorToInt(distanceFallen)}m";
            }
            else
            {
                // if Player GameObject got destroyed in DeletePlayerOnCollision script, this will save their last Y POS.
                panelBarsText[i].text = $"{Mathf.FloorToInt(lastKnownDistances[i])}m";
            }
        }
    }
}
