using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "baza/ItemInfo")]
public class SO_ItemInfo : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
}

public enum ItemType
{
    None,
    IronOre,
    Iron,
    Wood,
    Coal,
    Wrench,
    Patch,
    Battery
}