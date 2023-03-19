using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Fabrics
{
    
    public class Fabric: Building, IInteractable
    {
        public FabricState state;

        [SerializeField] private ItemStack inputItem;
        [SerializeField] private ItemStack outputItem;



        [SerializeField] private Cooldown buildingTime;
        [SerializeField] private Cooldown delayCooldown;

        public float craftTime = 3f;

        public SpriteFillable fillIndicator;
        public SpriteRenderer inputSprite;
        public SpriteRenderer outputSprite;

        public bool IsBusy => !buildingTime.IsReady && !delayCooldown.IsReady;

        public Vector2 popupPos => transform.position + Vector3.up * 0.5f;


        // TODO: add max buffer size

        private float craftProgress;

        public override void Init() {
            SO_ItemInfo inputItemInfo = Game.GetItemInfo(inputItem.type);
            SO_ItemInfo outputItemInfo = Game.GetItemInfo(outputItem.type);

            inputSprite.sprite = inputItemInfo.icon;
            outputSprite.sprite = outputItemInfo.icon;            
        }

        public override void Tick() {
            if(state == FabricState.IsCrafting) {
                craftProgress += Time.deltaTime / craftTime;
                fillIndicator.fillAmount = craftProgress;

                if(craftProgress >= 1.0f) {
                    OutputItem();
                }
            } else if (state == FabricState.Idle) {
                fillIndicator.fillAmount = 0f;
            }
        }

        public void OutputItem()
        {
            Game.inst.SpawnItem(outputItem.type, transform.position + Vector3.right * 1f);
            // TODO: Add item object to scene
            state = FabricState.Idle;
            craftProgress = 0f;
            fillIndicator.fillAmount = 0f;

            if(outputItem.type == ItemType.Iron || outputItem.type == ItemType.Patch || outputItem.type == ItemType.Wrench) {
                MSound.Play("anvil_hit", transform.position, 1.0f);
            }
        }

        /*
        private void OnTriggerStay2D(Collider2D collision) {
            if(state == FabricState.Idle) {
                PlayerLogic playerLogic = collision.GetComponent<PlayerLogic>();

                if(playerLogic != null) {
                    if(Game.inst.inventory.TryTakeItem(inputItem.type)) {

                        if (Input.GetKey(KeyCode.E)) {
                            Game.inst.inventory.TakeItem(inputItem.type);

                            craftProgress = 0f;
                            state = FabricState.IsCrafting;
                        }
                    }
                }
            }
        }
        */

        public void Interact() {
            if (state == FabricState.Idle) {
                if (Game.inst.inventory.TryTakeItem(inputItem.type)) {
                    Game.inst.inventory.TakeItem(inputItem.type);

                    craftProgress = 0f;
                    state = FabricState.IsCrafting;
                }
            }
        }

        public string InteractText() {
            return "Craft [E]";
        }
    }

    public enum FabricState {
        Idle,
        IsCrafting
    }
}