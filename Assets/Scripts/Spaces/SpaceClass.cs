using UnityEngine;
using System;
using System.Collections;
public class SpaceClass : MonoBehaviour
{

    public SpaceClass nextSpaces;
    public SpaceClass prevSpace;
    public Player player;
    public SpaceActions spaceAction;



    SpaceClass()
    {

    }


    private void Start()
    {
        spaceAction = GetComponent<SpaceActions>();

    }


}
