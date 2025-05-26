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

    private GameObject previouslyHighlightedObject = null;

    public void Action(Player player)
    {

        if (!(player.items.Count >= 3)) { 
        audioSource = GetComponent<AudioSource>();

        if (ShopWindowCamera != null)
            ShopWindowCamera.SetActive(true);

        StartCoroutine(Shop(player));
    }
        else { player.playerAction = false; }
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

      
        if (shopItem3DObjects != null && shopItem3DObjects.Count > 0)
        {
            foreach (var itemObj in shopItem3DObjects)
            {
                if (itemObj != null)
                {
                    Outline outline = itemObj.GetComponentInChildren<Outline>();
                    if (outline != null)
                        outline.enabled = false;
                }
            }

            previouslyHighlightedObject = shopItem3DObjects[0];
            Outline firstOutline = previouslyHighlightedObject.GetComponentInChildren<Outline>();
            if (firstOutline != null)
                firstOutline.enabled = true;
        }

        while (true)
        {
            int tempValue = ChooseOption.Choose(itemSelected, shop.Count, player.gamepad);
            if (itemSelected != tempValue)
            {
                itemSelected = tempValue;

               
                foreach (var itemObj in shopItem3DObjects)
                {
                    if (itemObj != null)
                    {
                        Outline outline = itemObj.GetComponentInChildren<Outline>();
                        if (outline != null)
                            outline.enabled = false;
                    }
                }

               
                if (shopItem3DObjects != null && itemSelected < shopItem3DObjects.Count)
                {
                    previouslyHighlightedObject = shopItem3DObjects[itemSelected];
                    Outline newOutline = previouslyHighlightedObject.GetComponentInChildren<Outline>();
                    if (newOutline != null)
                        newOutline.enabled = true;
                }

                Debug.Log(shop[itemSelected].name + " " + shop[itemSelected].price + "$");
            }

            if (ChooseOption.Select(player.gamepad))
            {
                print(player.money);

                if (player.money >= shop[itemSelected].price)
                {
                    player.items.Add((Item)shop[itemSelected].Clone());
                    player.money -= shop[itemSelected].price;
                    player.playerAction = false;

                    Debug.Log("You bought: " + shop[itemSelected].name + " Thank you come again");

            

                    if (shopItem3DObjects != null && itemSelected < shopItem3DObjects.Count)
                    {
                        shopItem3DObjects[itemSelected].SetActive(false);
                    }

                    if (shop[itemSelected].name == "Duff Beer" && BuyBeerClip != null)
                        audioSource.PlayOneShot(BuyBeerClip);
                    else if (shop[itemSelected].name == "D10" && BuyD10Clip != null)
                        audioSource.PlayOneShot(BuyD10Clip);
                    else if (shop[itemSelected].name == "D20" && ThankYouComeAgainClip != null)
                            audioSource.PlayOneShot(ThankYouComeAgainClip);

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
            Input.ResetInputAxes();

            yield return null;
        }

        // Disable outline when shop closes
        foreach (var itemObj in shopItem3DObjects)
        {
            if (itemObj != null)
            {
                Outline outline = itemObj.GetComponentInChildren<Outline>();
                if (outline != null)
                    outline.enabled = false;
            }
        }

        if (ShopWindowCamera != null)
            ShopWindowCamera.SetActive(false);
    }
}
