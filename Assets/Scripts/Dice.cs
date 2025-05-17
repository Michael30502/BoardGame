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
    public void StartRolling(int dieType)
    {
        isRolling = true;
        StartCoroutine(LoopDiceRoll(dieType));
    }

    public void StopRolling(int finalNumber,int dieType)
    {
        isRolling = false;
        rolledNumber = finalNumber;
        ApplyFinalRotation(finalNumber,dieType);
    }

    private IEnumerator LoopDiceRoll(int dieType)
    {
        int currentFace = 1;

        while (isRolling)
        {
            //Simply animation for Excitement before roll!
            ApplyFinalRotation(currentFace,dieType);
            currentFace = (currentFace % 6) + 1; 
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void ApplyFinalRotation(int number,int dieType)
    {
        Quaternion finalRotation = Quaternion.identity;

        switch (dieType) { 
            case 10:
                switch (number)
                {
                  
                    case 1: finalRotation = Quaternion.Euler(-180, -55, 0); break;
                    case 5: finalRotation = Quaternion.Euler(-180, 17, 0); break;
                    case 3: finalRotation = Quaternion.Euler(-180, 89, 0); break;
                    case 7: finalRotation = Quaternion.Euler(-180, 161, 0); break;
                    case 9: finalRotation = Quaternion.Euler(-180, 233, 0); break;

                  
                    case 10: finalRotation = Quaternion.Euler(0, 55, 0); break;
                    case 2: finalRotation = Quaternion.Euler(0, 127, 0); break;
                    case 6: finalRotation = Quaternion.Euler(0, 199, 0); break;
                    case 4: finalRotation = Quaternion.Euler(0, 271, 0); break;
                    case 8: finalRotation = Quaternion.Euler(0, 343, 0); break;
                }
                break;
            case 20:
                switch (number)
                {
                    case 1: finalRotation = Quaternion.Euler(-145, 166, -310); break;
                    case 2: finalRotation = Quaternion.Euler(8, 144.5f, 122.3f); break;
                    case 3: finalRotation = Quaternion.Euler(155.4f, 165, 189); break;
                    case 4: finalRotation = Quaternion.Euler(-9.8f, 201.4f, 165); break;
                    case 5: finalRotation = Quaternion.Euler(47.5f, -87.4f, 99.5f); break;
                    case 6: finalRotation = Quaternion.Euler(230.6f, -85.1f, 140.9f); break;
                    case 7: finalRotation = Quaternion.Euler(27.4f, -56.4f, 54.3f); break;
                    case 8: finalRotation = Quaternion.Euler(-16.3f, 124.9f, 4.7f); break;
                    case 9: finalRotation = Quaternion.Euler(161.2f, 125.1f, 55.9f); break;
                    case 10: finalRotation = Quaternion.Euler(350.6f, 87.9f, 81.9f); break;
                    case 11: finalRotation = Quaternion.Euler(166, 200, 444); break;
                    case 12: finalRotation = Quaternion.Euler(349.9f, 163.3f, 58.7f); break;
                    case 13: finalRotation = Quaternion.Euler(159.8f, 160, 5); break;
                    case 14: finalRotation = Quaternion.Euler(-13.8f, 162.89f, 240.2f); break;
                    case 15: finalRotation = Quaternion.Euler(-222.2f, 187.7f, 313); break;
                    case 16: finalRotation = Quaternion.Euler(-46.2f, 188.3f, 285.7f); break;
                    case 17: finalRotation = Quaternion.Euler(171.2f, 89.1f, 168.4f); break;
                    case 18: finalRotation = Quaternion.Euler(344.535f, 123.58f, 186.78f); break;
                    case 19: finalRotation = Quaternion.Euler(194.448f, -217.531f, 120.787f); break;
                    case 20: finalRotation = Quaternion.Euler(140, -15, 14); break;
                }

                break;

        default:
            switch (number)
            {
                case 1: finalRotation = Quaternion.Euler(270, 0, 0); break;
                case 2: finalRotation = Quaternion.Euler(0, 0, 0); break;
                case 3: finalRotation = Quaternion.Euler(0, 0, 270); break;
                case 4: finalRotation = Quaternion.Euler(0, 0, 90); break;
                case 5: finalRotation = Quaternion.Euler(180, 0, 0); break;
                case 6: finalRotation = Quaternion.Euler(90, 0, 0); break;
            } break;
        }
        transform.rotation = finalRotation;
    }
}
