using System.Collections.Generic;
using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public int rounds;
    public List<GameObject> players = new List<GameObject>();

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

}
