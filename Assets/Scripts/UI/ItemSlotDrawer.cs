using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotDrawer : MonoBehaviour
{
    public Image icon;
    public Image durabilityImage;

    public void Draw(ItemStack item)
    {
        if (item.type == ItemType.None)
        {
            icon.enabled = false;
            durabilityImage.enabled = false;
            return;
        }

        SO_ItemInfo info = Game.GetItemInfo(item.type);

        icon.enabled = true;
        icon.sprite = info.icon;

        durabilityImage.enabled = item.durability < 1.0f;
        durabilityImage.rectTransform.anchorMax = new Vector2(item.durability, 1f);
    }
}
