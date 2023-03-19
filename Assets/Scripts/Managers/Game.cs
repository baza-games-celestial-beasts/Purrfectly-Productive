using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Managers.Game_States;
using UI;


public class Game : MonoBehaviour
{
    public static Game inst;

    public SlotsUi slotsUi;
    public Inventory inventory;

    public ItemEntity itemEntityPrefab;

    private List<ItemEntity> items;
    private List<SO_ItemInfo> itemsInfo;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Tick();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ItemEntity item = SpawnItem(GameUtils.RandomItem(), pos);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < inventory.items.Length; i++)
            {
                inventory.DamageItem(inventory.items[i], 0.2f);
            }
        }

        slotsUi.Tick();
    }

    private void Init()
    {
        inst = this;

        itemsInfo = new List<SO_ItemInfo>(Resources.LoadAll<SO_ItemInfo>("Items"));
        items = new List<ItemEntity>();

        inventory.Init();
        slotsUi.Init();
    }

    public ItemEntity SpawnItem(ItemType itemType, Vector2 pos)
    {
        SO_ItemInfo info = GetItemInfo(itemType);

        ItemEntity itemEntity = Instantiate(itemEntityPrefab);
        itemEntity.transform.position = pos;
        itemEntity.Init(info);

        items.Add(itemEntity);
        return itemEntity;
    }

    public void DestroyItem(ItemEntity item)
    {
        items.Remove(item);
        Destroy(item.gameObject);
    }

    public static SO_ItemInfo GetItemInfo(ItemType type)
    {
        return inst.itemsInfo.Find(x => x.itemType == type);
    }

}

public static class GameUtils
{
    public static ItemType RandomItem()
    {
        return (ItemType)(1 + Random.Range(0, (int)ItemType.Count));
    }
} 