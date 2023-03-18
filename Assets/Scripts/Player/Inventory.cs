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
            items[i] = new ItemStack(ItemType.None);
        }
    }

    public bool CheckPutItem(ItemType itemType)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type == ItemType.None)
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
            if (items[i].type == ItemType.None)
            {
                items[i] = new ItemStack(itemType);
                GameUI.inst.DrawItems(items);
                return;
            }
        }
    }

    public bool TryTakeItem(ItemType itemType)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type == itemType)
            {
                return true;
            }
        }

        return false;
    }

    public ItemStack TakeItem(ItemType itemType)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type == itemType)
            {
                ItemStack result = items[i];
                items[i] = new ItemStack(ItemType.None);
                GameUI.inst.DrawItems(items);
                return items[i];
            }
        }

        return null;
    }

    public void DamageItem(ItemStack item, float damage)
    {
        if (item.type == ItemType.None)
            return;

        item.durability -= damage;

        if (item.durability <= 0.01f)
        {
            int ind = Array.IndexOf(items, item);
            items[ind] = new ItemStack(ItemType.None);
        }

        GameUI.inst.DrawItems(items);
    }

}

[System.Serializable]
public class ItemStack
{
    public ItemType type { get; private set; }
    public float durability = 1.0f;

    public ItemStack(ItemType itemType)
    {
        type = itemType;
    }

}