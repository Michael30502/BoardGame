using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageDisplayPicker : MonoBehaviour
{
    public Player player;

    [Header("UI Images for item slots")]
    public List<Image> itemSlotImages = new List<Image>();

    [Header("UI Outlines matching each item slot (must be UnityEngine.UI.Outline)")]
    public UnityEngine.UI.Outline[] itemSlotOutlines = new UnityEngine.UI.Outline[3];


    [Header("Sprites mapped to item names")]
    public Sprite beerSprite;
    public Sprite d10Sprite;
    public Sprite d20Sprite;

    private Dictionary<string, Sprite> itemSpriteMap;
    private int selectedSlot = 0;

    private void Awake()
    {
        itemSpriteMap = new Dictionary<string, Sprite>
        {
            { "Duff Beer", beerSprite },
            { "D10", d10Sprite },
            { "D20", d20Sprite }
        };

        UpdateSlotOutlines();
    }

    private void Update()
    {
        if (player == null) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedSlot = (selectedSlot + 1) % player.items.Count;
            UpdateSlotOutlines();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedSlot = (selectedSlot - 1 + player.items.Count) % player.items.Count;
            UpdateSlotOutlines();
        }

        UpdateItemSlotImages(); //  always called, like when it worked
    }


    private void UpdateItemSlotImages()
    {
        for (int i = 0; i < itemSlotImages.Count; i++)
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
                    Debug.LogWarning($"No sprite mapped for item '{itemName}'");
                    itemSlotImages[i].enabled = false;
                }
            }
            else
            {
                itemSlotImages[i].enabled = false;
            }
        }
    }



    private void UpdateSlotOutlines()
    {
        for (int i = 0; i < itemSlotOutlines.Length; i++)
        {
            if (itemSlotOutlines[i] != null)
            {
                itemSlotOutlines[i].enabled = (i == selectedSlot);
            }
        }
    }
}
