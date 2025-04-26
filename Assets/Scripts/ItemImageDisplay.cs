using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageDisplay : MonoBehaviour
{
    
    public Player player;

    [Header("UI Images for item slots, so players can have the same item twice as well!")]
    public Image[] itemSlotImages = new Image[3];

    [Header("Sprites mapped to item names")]
    public Sprite beerSprite;
    public Sprite d10Sprite;
    public Sprite d20Sprite;

    private Dictionary<string, Sprite> itemSpriteMap;

    private void Awake()
    {
        //Possibilties from items sorrt, ez fix!
        itemSpriteMap = new Dictionary<string, Sprite>
        {
            { "Duff Beer", beerSprite },
            { "D10", d10Sprite },
            { "D20", d20Sprite }
        };
    }

    private void Update()
    {
        UpdateItemSlotImages();
    }

    private void UpdateItemSlotImages()
    {
        for (int i = 0; i < itemSlotImages.Length; i++)
        {
            if (i < player.items.Count)
            {
                string itemName = player.items[i].name;

                if (itemSpriteMap.TryGetValue(itemName, out Sprite sprite))
                {
                    itemSlotImages[i].sprite = sprite;
                    itemSlotImages[i].enabled = true;
                }
                else
                {
                    Debug.LogWarning($"No sprite mapped for this itemerino '{itemName}'");
                    itemSlotImages[i].enabled = false;
                }
            }
            else
            {
                itemSlotImages[i].enabled = false;
            }
        }
    }
}
