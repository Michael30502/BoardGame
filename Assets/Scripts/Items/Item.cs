using System;
using UnityEngine;

public class Item: MonoBehaviour,ICloneable
{

    //Cloneable is used to ensure, that when you buy an item you buy a copy and not the element from the store

    public new string name;
    public string Name{
        set { name = value; }
        get { return name; }
    }

    public int price;
    public int Price
    {
        set { price = value; }
        get { return price; }
    }

    //Virtual is used so the action can be overwritten
    public virtual void Action(Player player){


    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
