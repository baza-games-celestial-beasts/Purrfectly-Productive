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
            }
        }

    }
}
