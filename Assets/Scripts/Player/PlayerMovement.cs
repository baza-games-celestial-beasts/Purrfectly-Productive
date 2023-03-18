using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float speed;
        [SerializeField] private string horizontalAxis = "Horizontal";
        [SerializeField] private string verticalAxis = "Vertical";
        [SerializeField] private PlayerAnimator playerAnimator;

        private Rigidbody2D _rigidbody2D;

        #endregion

        #region Monobehaviour Callbacks

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        #endregion


        private void Move()
        {
            var horizontalMove = Input.GetAxis(horizontalAxis);
            var verticalMove = Input.GetAxis(verticalAxis);

            var isMove = !(horizontalMove == 0 && verticalMove == 0);
            playerAnimator.SetIsMoveState(isMove);
            if (isMove)
            {
                playerAnimator.gameObject.transform.localScale = new Vector3(horizontalMove > 0 ? 1 : -1, 1, 1);
            }

            _rigidbody2D.velocity = new Vector2(horizontalMove, verticalMove) * speed;
        }
    }
}