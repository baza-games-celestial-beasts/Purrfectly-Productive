using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Generator_Logic
{
    [RequireComponent(typeof(Animator))]
    public class GeneratorItem : MonoBehaviour, IInteractable
    {
        #region Variables
        [SerializeField, ReadOnly] 
        private GeneratorItemState currentState;
        [SerializeField] private Transform popUpTransform;
        [Space(10)]
        [SerializeField, TextArea] private string requiredItemText;
        [SerializeField, TextArea] private string interactText;
        [SerializeField] private ItemType targetItem;
        [SerializeField] private GameObject patch;
        
        public Vector2 popupPos => popUpTransform.position + Vector3.up * 0.5f;

        private Animator _animator;
        private static readonly int IsBroken = Animator.StringToHash("IsBroken");
        private static readonly int IsHealthy = Animator.StringToHash("IsHealthy");

        private Collider2D _interactionCollider;

        private bool CanFix => Game.inst.inventory.TryTakeItem(targetItem);
        
        public event Action<GeneratorItem> OnFixed;
        public event Action<GeneratorItem> OnBroken; 
        #endregion
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _interactionCollider = GetComponent<Collider2D>();
            
            ChangeState(GeneratorItemState.Health);
        }

        #region States Logic
        public void ChangeState(GeneratorItemState targetState)
        {
            currentState = targetState;

            _interactionCollider.enabled = currentState == GeneratorItemState.Broken;
            UpdatePatch();

            switch (targetState)
            {
                case GeneratorItemState.Fixed:
                case GeneratorItemState.Health:
                    Fix();
                    break;
                case GeneratorItemState.Broken:
                    Break();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
            }
        }
        
        private void Break()
        {
            _animator.SetTrigger(IsBroken);
            OnBroken?.Invoke(this);
        }

        private void Fix()
        {
            _animator.SetTrigger(IsHealthy);
            OnFixed?.Invoke(this);
        }
        #endregion
        
        // For test only!!
        [Button("Fix item"), EnableIf(nameof(currentState), GeneratorItemState.Broken)]
        private void SetStateToFixed()
        {
            ChangeState(GeneratorItemState.Fixed);
        }
        
        private void UpdatePatch()
        {
            if (patch != null)
            {
                patch.SetActive(currentState == GeneratorItemState.Fixed);
            }
        }
        
        public void Interact()
        {
            if (currentState != GeneratorItemState.Broken) return;
            
            if (CanFix) {
                Game.inst.inventory.TakeItem(targetItem);

                ChangeState(GeneratorItemState.Fixed);
            }
        }

        public string InteractText()
        {
            return CanFix ? interactText : requiredItemText;
        }
    }
}
