using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotDrawer : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Image durabilityImage;

    public Action<ItemSlotDrawer> OnClick;
    public ItemStack currentItem { get; private set; }

    public void Draw(ItemStack item)
    {
        if (item.type == ItemType.None)
        {
            icon.enabled = false;
            durabilityImage.enabled = false;
            currentItem = null;
            return;
        }

        currentItem = item;
        SO_ItemInfo info = Game.GetItemInfo(item.type);

        icon.enabled = true;
        icon.sprite = info.icon;

        durabilityImage.enabled = item.durability < 1.0f;
        durabilityImage.rectTransform.anchorMax = new Vector2(item.durability, 1f);
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(this);
    }
}
