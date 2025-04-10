using System;
using UnityEngine;

public class Item: MonoBehaviour,ICloneable
{
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

    public virtual void Action(Player player){


    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
