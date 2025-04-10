using System.Collections.Generic;
using System.Xml.Linq;
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
