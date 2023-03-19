using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ItemEntity : MonoBehaviour, IInteractable
{
    public Transform iconT;
    public SpriteRenderer spriteRend;

    private float spawnTime;

    public ItemType itemType { get; private set; }

    public Vector2 popupPos => iconT.position + Vector3.up * 0.5f;

    public bool doFloatAnimation = true;

    public Action<ItemEntity> OnItemPickup;

    public void Init(SO_ItemInfo info)
    {
        spriteRend.sprite = info.icon;
        spawnTime = Time.time;
        itemType = info.itemType;
    }

    public void Tick()
    {
        float t = (Time.time - spawnTime);

        if (doFloatAnimation) {
            iconT.localPosition = Vector3.up * 0.5f * (1 + Mathf.Sin(t * 2f)) * 0.2f;
        } else {
            iconT.localPosition = Vector3.zero;
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null)
        {
            if(Game.inst.inventory.TryPutItem(itemType))
            {
                if (Input.GetKey(KeyCode.E)) {

                    Game.inst.inventory.PutItem(itemType);
                    OnItemPickup?.Invoke(this);

                    Game.inst.DestroyItem(this);
                }
            }
        }
    }
    */

    public void Interact() {
        if (Game.inst.inventory.TryPutItem(itemType)) {
            Game.inst.inventory.PutItem(itemType);
            OnItemPickup?.Invoke(this);

            Game.inst.DestroyItem(this);
        }
    }
}
