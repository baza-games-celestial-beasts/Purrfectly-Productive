using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Generator_Logic
{
    [RequireComponent(typeof(Animator))]
    public class GeneratorItem : MonoBehaviour, IInteractable
    {
        #region Variables
        [Header("Settings")]
        [SerializeField, ReadOnly] 
        private GeneratorItemState currentState;        
        [Space(10)]
        [SerializeField, TextArea] private string requiredItemText;
        [SerializeField, TextArea] private string interactText;
        [SerializeField] private ItemType targetItem;
        [SerializeField] private bool canFixOnLadderOnly;
        [SerializeField] private bool blinkOnBroken;

        [Header("Links")]
        [SerializeField] private GameObject patch;
        [SerializeField] private SpriteRenderer rend;
        [SerializeField] private Transform popUpTransform;

        public Vector2 popupPos => popUpTransform.position + Vector3.up * 0.5f;

        private Animator _animator;
        private static readonly int IsBroken = Animator.StringToHash("IsBroken");
        private static readonly int IsHealthy = Animator.StringToHash("IsHealthy");

        private Collider2D _interactionCollider;

        private bool CanFix => Game.inst.inventory.TryTakeItem(targetItem) && CheckLadder;
        private bool CheckLadder => !canFixOnLadderOnly || (canFixOnLadderOnly && Game.inst.player.isOnLadder);

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

        // For test only!!
        [Button("Break item")]
        private void SetStateToBroken() {
            ChangeState(GeneratorItemState.Broken);
        }

        private void Update() {
            if (blinkOnBroken) {
                if (currentState == GeneratorItemState.Broken) {
                    float t = (Mathf.Sin(Time.time * 7f) + 1) * 0.5f;
                    rend.color = Color.Lerp(Color.white, Color.red, t * 0.4f);
                } else {
                    rend.color = Color.white;
                }
            }
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
            if (!CheckLadder)
                return "";

            return CanFix ? interactText : requiredItemText;
        }
    }
}
