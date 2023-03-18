using System;
using Items;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerInventory: MonoBehaviour
    {
        [SerializeField] private int inventorySize = 5;
        
        private Item[] items;

        private void Start()
        {
            items = new Item[inventorySize];
        }

        public bool Contains(Item item) => items.Contains(item);

        public Item Pop(Item item)
        {
            int i;
            for (i = 0; i < inventorySize; i++)
            {
                if (item.Equals(items[i]))
                {
                    break;
                }
            }

            var res = items[i];
            if (i < inventorySize)
            {
                items[i] = Item.Empty;
            }

            return res;
        }

        public bool Push(Item item)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (items[i].Equals(Item.Empty))
                {
                    items[i] = item;
                    return true;
                }
            }

            return false;
        }
    }
}