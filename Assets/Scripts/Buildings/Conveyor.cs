using Fabrics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Building {

    public ItemStack outputItem;
    public float spawnTime = 2.0f;
    public int itemBufferSize = 3;

    public Transform startPoint;
    public Transform endPoint;

    [SerializeField] private List<ConveyorItem> items = new List<ConveyorItem>();
    private float spawnItemProgress;

    public override void Init() {
        base.Init();
    }

    public override void Tick() {
        if(items.Count < itemBufferSize) {
            spawnItemProgress += Time.deltaTime / spawnTime;

            if(spawnItemProgress >= 1.0f) {
                ConveyorItem conveyorItem = new ConveyorItem();
                conveyorItem.itemEntity = Game.inst.SpawnItem(outputItem.type, startPoint.position);
                conveyorItem.itemEntity.OnItemPickup += (item) => {
                    OnPickupItem(conveyorItem);
                };

                conveyorItem.itemEntity.doFloatAnimation = false;
                conveyorItem.conveyorPositionPercent = 0.0f;

                items.Add(conveyorItem);

                spawnItemProgress = 0.0f;
            }
        }

        for (int i = 0; i < items.Count; i++) {
            float endP = 1.0f - i / (float)(itemBufferSize - 1);

            if (items[i].conveyorPositionPercent < endP) {
                items[i].conveyorPositionPercent += Time.deltaTime;
            }

            items[i].itemEntity.transform.position = Vector2.Lerp(startPoint.position, endPoint.position, items[i].conveyorPositionPercent);
        }
    }

    private void OnPickupItem(ConveyorItem item) {
        items.Remove(item);
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerLogic playerLogic = collision.GetComponent<PlayerLogic>();

        if (playerLogic != null) {
            if (Game.inst.inventory.TryPutItem(outputItem.type)) {
                Game.inst.inventory.PutItem(outputItem.type);
                items.RemoveAt(0);
            }
        }
    }
    */

    [System.Serializable]
    public class ConveyorItem {
        public float conveyorPositionPercent;
        public ItemEntity itemEntity;
    }

}
