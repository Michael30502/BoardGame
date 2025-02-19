using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UIElements;
public class Player : MonoBehaviour
{

    public SpaceClass currentSpace;
    private int spaceToMove=0;
    private bool block = false;
    
    public SpaceClass makeChoice(ArrayList nextSpaces) {


        return (SpaceClass)nextSpaces[0];

    }

    public void moveNSpaces(int n) {

        StartCoroutine(SwapSpace(n));

    }

    private void Start()
    {
    }

    private void Update()
    {
        //print(currentSpace);
        Vector3 tempPos = currentSpace.transform.position;
        tempPos.y += 0.5f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, tempPos, 1 * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.Space) && block ==false)
        {
            int ran = UnityEngine.Random.Range(1, 7);
            print("Rolled" +ran);
            moveNSpaces(ran);
            block = true;
        }

    }


    IEnumerator SwapSpace(int n) {
        yield return new WaitForSeconds(1.0f);
        for (; n > 0; n--) { 
        
        currentSpace = currentSpace.nextSpaces;
            yield return new WaitForSeconds(2.0f);

        }
        block = false;
        print("ready");

    }


}


