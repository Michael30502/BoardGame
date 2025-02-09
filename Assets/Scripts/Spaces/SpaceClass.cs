using UnityEngine;
using System;
using System.Collections;
public class SpaceClass : MonoBehaviour
{

    public ArrayList nextSpaces;
    public SpaceClass prevSpace;
    public Player player;


    SpaceClass(ArrayList nextSpace, SpaceClass prevSpace,Player player) {
        this.nextSpaces = nextSpace;
        this.prevSpace = prevSpace;
        this.player = player;
    }
    
    public bool pathIsSplit(){
        if (nextSpaces.Count == 1) { 
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
    


    
}
