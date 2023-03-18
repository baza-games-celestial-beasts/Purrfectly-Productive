using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsMove = Animator.StringToHash("is-move");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIsMoveState(bool status)
    {
        _animator.SetBool(IsMove, status);
    }
}
