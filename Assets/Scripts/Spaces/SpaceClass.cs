using UnityEngine;
using System;
using System.Collections;
public class SpaceClass : MonoBehaviour
{

    public SpaceClass nextSpaces;
    public SpaceClass prevSpace;
    public Player player;


    SpaceClass(SpaceClass nextSpace, SpaceClass prevSpace,Player player) {
        this.nextSpaces = nextSpace;
        this.prevSpace = prevSpace;
        this.player = player;
    }
    /*
    public bool pathIsSplit(){
        if (nextSpaces == 1) { 
        return false;
        }
        else return true;
}
    
    public SpaceClass checkNext() {
        if (!pathIsSplit())
        {
            return (SpaceClass)nextSpaces[0];
        }
        else
        {
            return player.makeChoice(nextSpaces);

        }
    }
    */


    
}
