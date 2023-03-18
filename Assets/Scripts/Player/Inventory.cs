using System;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public ItemStack[] items;
    public const int INVENTORY_SIZE = 3;

    public void Init()
    {
        items = new ItemStack[INVENTORY_SIZE];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new ItemStack();
        }
    }

    public bool CheckPutItem(ItemType itemType)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].currentItem == ItemType.None)
            {
                return true;
            }
        }

        return false;
    }

    public void PutItem(ItemType itemType)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].currentItem == ItemType.None)
            {
                items[i].currentItem = itemType;
                return;
            }
        }
    }

}

public class ItemStack
{
    public ItemType currentItem;
}