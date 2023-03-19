using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int IsMove = Animator.StringToHash("is-move");
    private static readonly int IsClimb = Animator.StringToHash("is-climb");
    private static readonly int ClimbSpeed = Animator.StringToHash("climb-speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIsMoveState(bool status)
    {
        _animator.SetBool(IsMove, status);
    }

    public void SetIsClimbState(bool status) {
        _animator.SetBool(IsClimb, status);
    }

    public void SetClimbSpeed(float speed) {
        _animator.SetFloat(ClimbSpeed, speed);
    }

}
