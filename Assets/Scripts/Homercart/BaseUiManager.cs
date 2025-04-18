using UnityEngine;
using  UnityEngine.UI;


public class BaseUiManager : MonoBehaviour
{
    public int money;
    public Text moneyText;
    public int point;
    public Text pointText;
    public int position;
    public Text positionText;
    public Transform playerCamera;



    void Start()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

       
    }


    void Update()
    {
        transform.rotation = playerCamera.rotation;
        
        transform.position = playerCamera.position + new Vector3(2.9f, -3.7f, -0.1f);

    }
  
  public void addMoney(int newmoney)
  {
        money = newmoney;
        moneyText.text = "Money: " + money.ToString();
    }

    public void addPoint(int newpoint)
    {
        point = newpoint;
        pointText.text = "Point: " + point.ToString();
    }
 

    public void addPosition(int newposition)
    {
        position = newposition;
        positionText.text = "Position: " + position.ToString();
    }
}
