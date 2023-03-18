using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI inst;

    public ItemSlotDrawer[] slots;

    public void Init()
    {
        inst = this;
    }

    public void Tick()
    {

    }

    public void DrawItems(ItemStack[] items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Draw(items[i]);
        }
    }

}
