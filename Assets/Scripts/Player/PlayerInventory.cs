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

        public bool Contains(ItemType type) => items.Select(it => it.Type).Contains(type);
        
        public bool Push(Item item)
        {
            var i = Find(ItemType.Empty);
            if (i < inventorySize)
            {
                items[i] = item;
            }

            return i < inventorySize;
        }

        public void Use(ItemType type)
        {
            var i = Find(type);
            if (i >= inventorySize) return;
            
            if (!items[i].Use())
            {
                Pop(i);
            }
        }

        public Item Pop(ItemType type)
        {
            var i = Find(type);
            return i < inventorySize ? Pop(i) : null;
        }

        public Item Pop(int index)
        {
            var res = items[index];
            items[index] = null;
            return res;
        }

        public int Find(ItemType type)
        {
            int i;
            for (i = 0; i < inventorySize; i++)
            {
                if (items[i].Type == ItemType.Empty)
                {
                    break;
                }
            }

            return i;
        }

        public Item Get(int index) => items[index];
    }
}