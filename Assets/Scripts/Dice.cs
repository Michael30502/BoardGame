using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour
{
    public Transform player;
    private bool isRolling = false;
    private int rolledNumber = 1;


    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 2.5f, 0);
    }
    public void StartRolling()
    {
        isRolling = true;
        StartCoroutine(LoopDiceRoll());
    }

    public void StopRolling(int finalNumber)
    {
        isRolling = false;
        rolledNumber = finalNumber;
        ApplyFinalRotation(finalNumber);
    }

    private IEnumerator LoopDiceRoll()
    {
        int currentFace = 1;

        while (isRolling)
        {
            //Simply animation for Excitement before roll!
            ApplyFinalRotation(currentFace);
            currentFace = (currentFace % 6) + 1; 
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void ApplyFinalRotation(int number)
    {
        Quaternion finalRotation = Quaternion.identity;

        switch (number)
        {
            case 1: finalRotation = Quaternion.Euler(270, 0, 0); break;
            case 2: finalRotation = Quaternion.Euler(0, 0, 0); break;
            case 3: finalRotation = Quaternion.Euler(0, 0, 270); break;
            case 4: finalRotation = Quaternion.Euler(0, 0, 90); break;
            case 5: finalRotation = Quaternion.Euler(180, 0, 0); break;
            case 6: finalRotation = Quaternion.Euler(90, 0, 0); break;
        }

        transform.rotation = finalRotation;
    }
}
