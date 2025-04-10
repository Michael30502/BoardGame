using UnityEngine;

public class Item: MonoBehaviour
{
    public new string name;
    public string Name{
        set { name = value; }
        get { return name; }
}
    public virtual void Action(Player player){


    }


}
