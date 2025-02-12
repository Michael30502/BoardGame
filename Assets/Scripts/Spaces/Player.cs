using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UIElements;
public class Player : MonoBehaviour
{

    public SpaceClass currentSpace;
    private int spaceToMove=0;
    
    public SpaceClass makeChoice(ArrayList nextSpaces) {


        return (SpaceClass)nextSpaces[0];

    }

    public void moveNSpaces(int n) {

        spaceToMove = n;

    }

    private void Start()
    {

    }

    private void Update()
    {
        print(currentSpace);
        Vector3 tempPos = currentSpace.transform.position;
        tempPos.y += 0.5f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);
        StartCoroutine(SwapSpace());
        
        
    }


    IEnumerator SwapSpace() {
        yield return new WaitForSeconds(5.0f);
        currentSpace = currentSpace.nextSpaces;


    }

}
