using UnityEngine;
using UnityEngine.UI;

public class finishLine : MonoBehaviour
{
        public Image finishLinePicture; 

  private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))

            Debug.Log("Player finished the race!");
            //  MatchManager.Instance.EndGame(); // or whatever handles ending the match
      
    }}

