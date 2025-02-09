using UnityEngine;
using System;
using System.Collections;
public class Player : MonoBehaviour
{

    public SpaceClass currentSpace;

    Player(SpaceClass currentSpace) {
        this.currentSpace = currentSpace;
    }
    
    public SpaceClass makeChoice(ArrayList nextSpaces) {


        return (SpaceClass)nextSpaces[0];

    }
    
    
}
