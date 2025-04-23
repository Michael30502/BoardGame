using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class finishLine : MonoBehaviour
{

public GameObject[] finishCanvas;

void Start()
{
    foreach (GameObject canvas in finishCanvas)
    {
        canvas.SetActive(false);
    }
}

private void OnTriggerEnter(Collider other)
{
            Debug.Log("Trigger entered by: " ); // ⬅ Check if this shows up

    if (other.CompareTag("Player1"))
    {  
                Debug.Log("Player1 triggered finish!");

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


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("STAY: " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT: " + other.name);
    }
}

