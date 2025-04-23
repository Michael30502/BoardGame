using TMPro;
using UnityEngine;

public class HudChanger : MonoBehaviour
{
    public TurnController turnController;


    private void Start()
    {
        GameOptions gameOptions = null;
        try
        {
            gameOptions = GameObject.Find("GameOptions").GetComponent<GameOptions>();
        }
        catch { print("gameoption not found"); }
        if (gameOptions != null)
        {
            print(gameOptions.players.Count);

          
            for(int i =0; i< 4; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < gameOptions.players.Count; i++)
            {
              
                for (int i2=0; i2< gameOptions.players[i].transform.childCount; i2++)
                {
                    GameObject temp = gameOptions.players[i].transform.GetChild(i2).gameObject;
                    if(temp.tag == "Hud")
                    {
                        GameObject hudInstance = Instantiate(temp);
                        hudInstance.transform.position = new Vector3(((i * 478) - 732), -567, 0);
                        hudInstance.transform.SetParent(gameObject.transform, false);
                        hudInstance.SetActive(true);

                        HUDManager hUDManager = hudInstance.transform.parent.transform.parent.transform.parent.GetComponent<HUDManager>();

                        for (int i3 = 0; i3 < hudInstance.transform.childCount; i3++)
                        {

                            switch (hudInstance.transform.GetChild(i3).gameObject.name)
                            {
                                case "PlayerPlace":
                                    hUDManager.playerHUDs[i].rankText = hudInstance.transform.GetChild(i3).gameObject.GetComponent<TextMeshProUGUI>();
                                    break;
                                case "CoinIcon":
                                    hUDManager.playerHUDs[i].moneyText = hudInstance.transform.GetChild(i3).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                                    break;
                                case "DonutIcon":
                                    hUDManager.playerHUDs[i].pointText = hudInstance.transform.GetChild(i3).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

                                    break;
                                case "ItemPanel":

                                    hudInstance.transform.GetChild(i3).gameObject.GetComponent<ItemImageDisplay>().player = turnController.playerList[i];
;
                                    break;


                            }
                        }
                        hUDManager.UpdateRankings();
                        hUDManager.CacheCurrentStats();


                        break;
                    }
                }
            }
        }
    }
}
