using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    public Transform iconT;
    public SpriteRenderer spriteRend;

    private float spawnTime;

    public ItemType itemType { get; private set; }

    public void Init(SO_ItemInfo info)
    {
        spriteRend.sprite = info.icon;
        spawnTime = Time.time;
        itemType = info.itemType;
    }

    public void Tick()
    {
        float t = (Time.time - spawnTime);
        iconT.localPosition = Vector3.up * 0.5f * (1 + Mathf.Sin(t * 2f)) * 0.3f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null)
        {
            if(Game.inst.inventory.CheckPutItem(itemType))
            {
                Game.inst.inventory.PutItem(itemType);
                Game.inst.DestroyItem(this);
            }
        }
    }

}
