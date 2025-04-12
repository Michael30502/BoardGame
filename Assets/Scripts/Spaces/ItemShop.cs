using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour, SpaceActions
{
  
    public List<Item> shop = new List<Item>();

    [SerializeField] public GameObject ShopWindowCamera;

    [SerializeField] private List<GameObject> shopItem3DObjects;

    
    private AudioSource audioSource;

    [SerializeField] private AudioClip GreetingsCustomerClip;
    [SerializeField] private AudioClip ThankYouComeAgainClip;
    [SerializeField] private AudioClip BuyBeerClip;
    [SerializeField] private AudioClip BuyD10Clip;

   
    public void Action(Player player)
    {
        audioSource = GetComponent<AudioSource>();

        if (ShopWindowCamera != null)
            ShopWindowCamera.SetActive(true);

        StartCoroutine(Shop(player));
    }

    
    public bool GetCountSpace()
    {
        return false;
    }

   
    IEnumerator Shop(Player player)
    {
        Debug.Log("Buy Something!!!");

        if (GreetingsCustomerClip != null)
            audioSource.PlayOneShot(GreetingsCustomerClip);

        int itemSelected = 0;

        while (true)
        {
            int tempValue = ChooseOption.Choose(itemSelected, shop.Count, player.gamepad);
            if (itemSelected != tempValue)
            {
                itemSelected = tempValue;
                Debug.Log(shop[itemSelected].name + " " + shop[itemSelected].price + "$");
            }

            if (ChooseOption.Select(player.gamepad))
            {
                if (player.money >= shop[itemSelected].price)
                {
                    player.items.Add((Item)shop[itemSelected].Clone());
                    player.money -= shop[itemSelected].price;
                    player.playerAction = false;

                    Debug.Log("You bought: " + shop[itemSelected].name + " Thank you come again");

                   
                    if (ThankYouComeAgainClip != null)
                        audioSource.PlayOneShot(ThankYouComeAgainClip);

                    
                    if (shopItem3DObjects != null && itemSelected < shopItem3DObjects.Count)
                    {
                        shopItem3DObjects[itemSelected].SetActive(false);
                    }

                   
                    if (shop[itemSelected].name == "Duff Beer" && BuyBeerClip != null)
                        audioSource.PlayOneShot(BuyBeerClip);
                    else if (shop[itemSelected].name == "D10" && BuyD10Clip != null)
                        audioSource.PlayOneShot(BuyD10Clip);

                    break;
                }
                else
                {
                    Debug.Log("No money");
                }
            }

            if (ChooseOption.Cancel(player.gamepad))
            {
                player.playerAction = false;
                break;
            }

            yield return null;
        }

        if (ShopWindowCamera != null)
            ShopWindowCamera.SetActive(false);
    }
}
