using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Generator_Logic
{
    [RequireComponent(typeof(Animator))]
    public class GeneratorItem : MonoBehaviour
    {
        #region Variables
        [SerializeField, ReadOnly] 
        private GeneratorItemState currentState;

        [SerializeField] private GameObject patch;

        private Animator _animator;
        private static readonly int IsBroken = Animator.StringToHash("IsBroken");
        private static readonly int IsHealthy = Animator.StringToHash("IsHealthy");

        public event Action<GeneratorItem> OnFixed;
        public event Action<GeneratorItem> OnBroken; 
        #endregion
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            ChangeState(GeneratorItemState.Health);
        }

        #region States Logic
        public void ChangeState(GeneratorItemState targetState)
        {
            currentState = targetState;

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
    }
}
