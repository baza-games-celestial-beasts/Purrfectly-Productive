using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Fabrics
{
    
    public class Fabric: MonoBehaviour
    {
        [SerializeField] private bool isCraft;
        [SerializeField] private ItemType inputType;
        [SerializeField] private ItemType outputType;

        [SerializeField] private Cooldown buildingTime;
        [SerializeField] private Cooldown delayCooldown;

        public bool IsBusy => !buildingTime.IsReady && !delayCooldown.IsReady;

        [Inject] private Inventory _inventory;
        
        // TODO: add max buffer size

        public void Create()
        {
            if (IsBusy) return;

            bool start = true;
            if (isCraft)
            {
                start = _inventory.TryTakeItem(inputType);
            }

            if (start)
            {
                buildingTime.Reset();
                Invoke(nameof(Spawn), buildingTime.ValueOfCooldown);
            }
        }

        public void Spawn()
        {
            // TODO: Add item object to scene
            delayCooldown.Reset();
        }
    }
}