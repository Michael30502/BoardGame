using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class raceCountDown : MonoBehaviour
{
    public Sprite[] countdownSprites; // Assign your countdown images (e.g., 3, 2, 1, GO) in the Inspector
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
          StartCoroutine(PlayCountdown());
    }

    IEnumerator PlayCountdown()
    {
        for (int i = 0; i < countdownSprites.Length; i++)
        {
            img.sprite = countdownSprites[i]; // Change the image
            img.color = new Color(1f, 1f, 1f, 1f); // Make sure it's fully visible

            // Optional fade (just like before)
            yield return StartCoroutine(FadeOut());

            yield return new WaitForSeconds(0.1f); // Optional pause before next image
        }

        img.enabled = false; // Hide image after countdown
    }

    IEnumerator FadeOut()
    {
        Color c = img.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

  
}
