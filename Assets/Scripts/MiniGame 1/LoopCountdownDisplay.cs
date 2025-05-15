using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoopingCountdownDisplay : MonoBehaviour
{
    [Header("Countdown Sprites")]
    public Sprite[] countdownSprites;  // E.g. [3, 2, 1, GO]

    [Header("UI Image Target")]
    public Image countdownImage;       // The Image component to update

    [Header("Settings")]
    public float displayDuration = 0.5f; // How long each sprite shows
    public float fadeDuration = 0.3f;    // Optional fade out per sprite

    private void Start()
    {
        StartCoroutine(CountdownLoop());
    }

    IEnumerator CountdownLoop()
    {
        while (true)
        {
            for (int i = 0; i < countdownSprites.Length; i++)
            {
                countdownImage.sprite = countdownSprites[i];
                countdownImage.color = new Color(1f, 1f, 1f, 1f); // Full opacity

                yield return new WaitForSeconds(displayDuration);

                // Optional fade-out
                yield return StartCoroutine(FadeOut(countdownImage));
            }

            yield return new WaitForSeconds(0.2f); // Small pause before restarting
        }
    }

    IEnumerator FadeOut(Image img)
    {
        Color c = img.color;
        float step = 0.1f;
        for (float alpha = 1f; alpha >= 0f; alpha -= step)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(fadeDuration * step);
        }
    }
}
