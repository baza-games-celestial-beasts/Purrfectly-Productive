using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotDrawer : MonoBehaviour
{
    public Image icon;

    public void Draw(ItemStack itemSlot)
    {
        //SO_ItemInfo info = Game.GetItemInfo(itemSlot.currentItem);
        //icon.sprite = info.icon;
    }
}
