using UnityEngine;

namespace UI
{
    public class SlotsUi : MonoBehaviour
    {
        public static SlotsUi inst;

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

                int ind = i;
                slots[ind].OnClick += (slot) => {
                    Debug.Log(ind);
                    if (slot.currentItem != null) {
                        Debug.Log("A1");
                        if (Game.inst.inventory.TryTakeItem(slot.currentItem.type)) {
                            Debug.Log("A2");
                            ItemType itemType = slot.currentItem.type;
                            Game.inst.inventory.TakeItem(itemType);
                            Game.inst.SpawnItem(itemType, Game.inst.player.transform.position + Vector3.up * -0.5f + (Vector3)Random.insideUnitCircle * 0.3f);

                            MSound.Play("pickup_item", transform.position, 1.0f, 0.7f);
                        }
                    }
                };
            }
        }

    }
}
