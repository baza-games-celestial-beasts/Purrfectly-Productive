using System;
using Unity.VisualScripting;

namespace Items
{
    public class Item
    {
        public readonly ItemType Type;

        public Item(ItemType type)
        {
            Type = type;
        }

        public virtual bool Use()
        {
            return false;
        }

        public static Item Create(ItemType type)
        {
            switch (type)
            {
                case ItemType.Iron:
                case ItemType.Cool:
                case ItemType.Battery:
                case ItemType.Patch: 
                case ItemType.Wood: 
                case ItemType.IronPatch: 
                case ItemType.IronPlate:
                    return new Item(type);
                case ItemType.Wrench:
                    return new Wrench();
                default:
                    throw new UnexpectedEnumValueException<ItemType>(type);
            }
        }
    }
}