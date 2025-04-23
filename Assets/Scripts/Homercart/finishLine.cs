using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class finishLine : MonoBehaviour
{

    public GameObject[] finishCanvas;
    private int counter;

    void Start()
    {
        foreach (GameObject canvas in finishCanvas)
        {
            canvas.SetActive(false);
        }
    }
    void Update()
    {
        if (counter >= 3)
        {
            Debug.Log("All players have finished!");
            // You can add additional logic here, such as ending the game or displaying a final screen.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger stay with:  enter");
        if (other.CompareTag("Player1"))
        {
            Debug.Log("Player1 triggered finish!");
            counter++;

            finishCanvas[0].SetActive(true);
        }
        if (other.CompareTag("Player2"))
        {
            finishCanvas[1].SetActive(true);
        }
        if (other.CompareTag("Player3"))
        {
            finishCanvas[2].SetActive(true);
        }
        if (other.CompareTag("Player4"))
        {
            finishCanvas[3].SetActive(true);
        }

    }


}

