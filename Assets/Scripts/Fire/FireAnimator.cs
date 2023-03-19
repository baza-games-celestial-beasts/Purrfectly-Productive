using Generator_Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fire
{
    public class FireAnimator: MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField] private AnimatorOverrideController hotFireController;
        [SerializeField] private AnimatorOverrideController coldFireController;
        
        [SerializeField] private GeneratorItem wing;
        [SerializeField] private GeneratorItem thermometer;
        
        private static readonly int Active = Animator.StringToHash("Active");

        private void Start()
        {
            _animator = GetComponent<Animator>();

            wing.OnBroken += PutOutFire;
            wing.OnFixed += SetOnFire;

            thermometer.OnBroken += ToColdFire;
            thermometer.OnFixed += ToHotFire;
        }

        private void PutOutFire(GeneratorItem _)
        {
            _animator.SetBool(Active, false);
        }

        private void SetOnFire(GeneratorItem _)
        {
            _animator.SetBool(Active, true);
        }

        private void ToColdFire(GeneratorItem _)
        {
            Debug.Log("ToColdFire");
            _animator.runtimeAnimatorController = coldFireController;
        }

        private void ToHotFire(GeneratorItem _)
        {
            Debug.Log("ToHotFire");
            _animator.runtimeAnimatorController = hotFireController;
        }
    }
}