using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Managers.Game_States;


public class Game : MonoBehaviour
{
    public static Game inst;

    public GameUI gameUI;

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
            ItemEntity item = SpawnItem(ItemType.Battery, pos);
        }

        gameUI.Tick();
    }

    private void Init()
    {
        inst = this;

        itemsInfo = new List<SO_ItemInfo>(Resources.LoadAll<SO_ItemInfo>("Items"));
        items = new List<ItemEntity>();

        gameUI.Init();
    }

    public ItemEntity SpawnItem(ItemType itemType, Vector2 pos)
    {
        ItemEntity itemEntity = Instantiate(itemEntityPrefab);
        itemEntity.transform.position = pos;
        items.Add(itemEntity);
        return itemEntity;
    }

    public static SO_ItemInfo GetItemInfo(ItemType type)
    {
        return inst.itemsInfo.Find(x => x.itemType == type);
    }

}
